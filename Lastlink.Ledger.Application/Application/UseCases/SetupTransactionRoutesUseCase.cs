using System.Net;
using Lastlink.Ledger.Application.Contracts;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupTransactionRoutesUseCase : ISetupTransactionRoutesUseCase
{
    private readonly ITransactionRouteApi _api;
    private readonly List<CreateTransactionRouteRequest> _payloads;
    private readonly ILogger<SetupTransactionRoutesUseCase> _logger;

    public SetupTransactionRoutesUseCase(
        ITransactionRouteApi api,
        List<CreateTransactionRouteRequest> payloads,
        ILogger<SetupTransactionRoutesUseCase> logger)
    {
        _api = api;
        _payloads = payloads;
        _logger = logger;
    }

    public async Task<IReadOnlyList<SetupStepResult<TransactionRoute>>> ExecuteAsync(
        string organizationId,
        string ledgerId,
        IReadOnlyList<SetupStepResult<OperationRoute>> operationRouteResults)
    {
        _logger.LogInformation("── Step 6: Transaction Routes ───────────────────────────────");

        var codeToId = operationRouteResults
            .Where(r => r.Resource.Code is not null && r.Resource.Id is not null)
            .ToDictionary(r => r.Resource.Code!, r => r.Resource.Id!);

        var existing = await FetchExistingRoutesAsync(organizationId, ledgerId);
        var results = new List<SetupStepResult<TransactionRoute>>();

        foreach (var payload in _payloads)
        {
            var found = existing.Items.FirstOrDefault(r =>
                string.Equals(r.Title, payload.Title, StringComparison.OrdinalIgnoreCase));

            if (found is not null)
            {
                _logger.LogWarning("  Ja cadastrado (title: \"{Title}\")", payload.Title);
                results.Add(new SetupStepResult<TransactionRoute>(found, WasCreated: false));
                continue;
            }

            var resolvedIds = payload.OperationRoutes
                .Select(code => codeToId.TryGetValue(code, out var id) ? id : null)
                .Where(id => id is not null)
                .Cast<string>()
                .ToList();

            var apiRequest = payload with { OperationRoutes = resolvedIds };

            try
            {
                var created = await _api.CreateTransactionRouteAsync(organizationId, ledgerId, apiRequest);
                _logger.LogInformation("  Criado [{Title}] — ID: {Id}", created.Title, created.Id);
                results.Add(new SetupStepResult<TransactionRoute>(created, WasCreated: true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "  Erro ao criar transaction route (title: \"{Title}\")", payload.Title);
                throw;
            }
        }

        return results;
    }

    private async Task<PagedResult<TransactionRoute>> FetchExistingRoutesAsync(
        string organizationId,
        string ledgerId)
    {
        try
        {
            return await _api.GetTransactionRoutesAsync(organizationId, ledgerId);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhuma transaction route encontrada (404).");
            return new PagedResult<TransactionRoute>();
        }
    }
}
