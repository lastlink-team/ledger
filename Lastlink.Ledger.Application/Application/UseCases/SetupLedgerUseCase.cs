using System.Net;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Lastlink.Ledger.Application.Contracts;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupLedgerUseCase : ISetupLedgerUseCase
{
    private readonly ILedgerApi _api;
    private readonly CreateLedgerRequest _payload;
    private readonly ILogger<SetupLedgerUseCase> _logger;

    public SetupLedgerUseCase(
        ILedgerApi api,
        CreateLedgerRequest payload,
        ILogger<SetupLedgerUseCase> logger)
    {
        _api = api;
        _payload = payload;
        _logger = logger;
    }

    public async Task<SetupStepResult<LedgerInfo>> ExecuteAsync(string organizationId)
    {
        _logger.LogInformation("── Step 2: Ledger ───────────────────────────────────────────");

        var existing = await FetchExistingLedgersAsync(organizationId);
        var found = existing.Items.FirstOrDefault(l => l.Name == _payload.Name);

        if (found is not null)
        {
            _logger.LogWarning("  Já cadastrado (name: \"{Name}\")", _payload.Name);
            _logger.LogInformation("  Ledger ID: {Id}", found.Id);
            return new SetupStepResult<LedgerInfo>(found, WasCreated: false);
        }

        var created = await _api.CreateLedgerAsync(organizationId, _payload);
        _logger.LogInformation("  Criado — ID: {Id}", created.Id);
        return new SetupStepResult<LedgerInfo>(created, WasCreated: true);
    }

    private async Task<PagedResult<LedgerInfo>> FetchExistingLedgersAsync(string organizationId)
    {
        try
        {
            return await _api.GetLedgersAsync(organizationId);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhum ledger encontrado (404).");
            return new PagedResult<LedgerInfo>();
        }
    }
}
