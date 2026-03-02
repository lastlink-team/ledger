using System.Net;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Lastlink.Ledger.Application.Contracts;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupOrganizationUseCase : ISetupOrganizationUseCase
{
    private readonly IOrganizationApi _api;
    private readonly CreateOrganizationRequest _payload;
    private readonly ILogger<SetupOrganizationUseCase> _logger;

    public SetupOrganizationUseCase(
        IOrganizationApi api,
        CreateOrganizationRequest payload,
        ILogger<SetupOrganizationUseCase> logger)
    {
        _api = api;
        _payload = payload;
        _logger = logger;
    }

    public async Task<SetupStepResult<Organization>> ExecuteAsync()
    {
        _logger.LogInformation("── Step 1: Organização ──────────────────────────────────────");

        var existing = await FetchExistingOrganizationsAsync();
        var found = existing.Items.FirstOrDefault(o => o.LegalDocument == _payload.LegalDocument);

        if (found is not null)
        {
            _logger.LogWarning("  Já cadastrada (legalDocument: {LegalDocument})", _payload.LegalDocument);
            _logger.LogInformation("  Organization ID: {Id}", found.Id);
            return new SetupStepResult<Organization>(found, WasCreated: false);
        }

        var created = await _api.CreateOrganizationAsync(_payload);
        _logger.LogInformation("  Criada — ID: {Id}", created.Id);
        return new SetupStepResult<Organization>(created, WasCreated: true);
    }

    private async Task<PagedResult<Organization>> FetchExistingOrganizationsAsync()
    {
        try
        {
            return await _api.GetOrganizationsAsync();
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhuma organização encontrada (404).");
            return new PagedResult<Organization>();
        }
    }
}
