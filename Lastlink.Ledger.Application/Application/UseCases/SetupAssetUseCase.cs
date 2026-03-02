using System.Net;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Lastlink.Ledger.Application.Contracts;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupAssetUseCase : ISetupAssetUseCase
{
    private readonly IAssetApi _api;
    private readonly CreateAssetRequest _payload;
    private readonly ILogger<SetupAssetUseCase> _logger;

    public SetupAssetUseCase(
        IAssetApi api,
        CreateAssetRequest payload,
        ILogger<SetupAssetUseCase> logger)
    {
        _api = api;
        _payload = payload;
        _logger = logger;
    }

    public async Task<SetupStepResult<Asset>> ExecuteAsync(string organizationId, string ledgerId)
    {
        _logger.LogInformation("── Step 3: Asset ────────────────────────────────────────────");

        var existing = await FetchExistingAssetsAsync(organizationId, ledgerId);
        var found = existing.Items.FirstOrDefault(a => a.Code == _payload.Code);

        if (found is not null)
        {
            _logger.LogWarning("  Já cadastrado (code: \"{Code}\")", _payload.Code);
            _logger.LogInformation("  Asset ID: {Id}", found.Id);
            return new SetupStepResult<Asset>(found, WasCreated: false);
        }

        var created = await _api.CreateAssetAsync(organizationId, ledgerId, _payload);
        _logger.LogInformation("  Criado — ID: {Id}", created.Id);
        return new SetupStepResult<Asset>(created, WasCreated: true);
    }

    private async Task<PagedResult<Asset>> FetchExistingAssetsAsync(string organizationId, string ledgerId)
    {
        try
        {
            return await _api.GetAssetsAsync(organizationId, ledgerId);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhum asset encontrado (404).");
            return new PagedResult<Asset>();
        }
    }
}
