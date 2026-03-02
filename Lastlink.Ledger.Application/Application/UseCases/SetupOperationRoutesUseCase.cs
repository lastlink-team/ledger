using System.Net;
using Lastlink.Ledger.Application.Contracts;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupOperationRoutesUseCase : ISetupOperationRoutesUseCase
{
    private readonly IOperationRouteApi _api;
    private readonly List<CreateOperationRouteRequest> _payloads;
    private readonly ILogger<SetupOperationRoutesUseCase> _logger;

    public SetupOperationRoutesUseCase(
        IOperationRouteApi api,
        List<CreateOperationRouteRequest> payloads,
        ILogger<SetupOperationRoutesUseCase> logger)
    {
        _api = api;
        _payloads = payloads;
        _logger = logger;
    }

    public async Task<IReadOnlyList<SetupStepResult<OperationRoute>>> ExecuteAsync(
        string organizationId,
        string ledgerId)
    {
        _logger.LogInformation("── Step 5: Operation Routes ─────────────────────────────────");

        var existing = await FetchExistingRoutesAsync(organizationId, ledgerId);
        var results = new List<SetupStepResult<OperationRoute>>();

        foreach (var payload in _payloads)
        {
            var found = existing.Items.FirstOrDefault(r => r.Code == payload.Code);

            if (found is not null)
            {
                _logger.LogWarning("  Ja cadastrado (code: \"{Code}\")", payload.Code);
                results.Add(new SetupStepResult<OperationRoute>(found, WasCreated: false));
                continue;
            }

            try
            {
                
                        var created = await _api.CreateOperationRouteAsync(organizationId, ledgerId, payload);
                        _logger.LogInformation("  Criado [{Code}] — ID: {Id}", created.Code, created.Id);
                        results.Add(new SetupStepResult<OperationRoute>(created, WasCreated: true));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "  Erro ao criar operation route (code: \"{Code}\")", payload.Code);
                throw;
            }
        }

        return results;
    }

    private async Task<PagedResult<OperationRoute>> FetchExistingRoutesAsync(
        string organizationId,
        string ledgerId)
    {
        try
        {
            return await _api.GetOperationRoutesAsync(organizationId, ledgerId);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhuma operation route encontrada (404).");
            return new PagedResult<OperationRoute>();
        }
    }
}
