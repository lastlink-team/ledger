using System.Net;
using Lastlink.Ledger.Application.Contracts;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Microsoft.Extensions.Logging;
using Refit;

namespace Lastlink.Ledger.Application.UseCases;

public class SetupAccountTypeUseCase : ISetupAccountTypeUseCase
{
    private readonly IAccountTypeApi _api;
    private readonly CreateAccountTypeRequest _payload;
    private readonly ILogger<SetupAccountTypeUseCase> _logger;

    public SetupAccountTypeUseCase(
        IAccountTypeApi api,
        CreateAccountTypeRequest payload,
        ILogger<SetupAccountTypeUseCase> logger)
    {
        _api = api;
        _payload = payload;
        _logger = logger;
    }

    public async Task<SetupStepResult<AccountType>> ExecuteAsync(string organizationId, string ledgerId)
    {
        _logger.LogInformation("── Step 4: Account Type ─────────────────────────────────────");

        var existing = await FetchExistingAccountTypesAsync(organizationId, ledgerId);
        var found = existing.Items.FirstOrDefault(a => a.KeyValue == _payload.KeyValue);

        if (found is not null)
        {
            _logger.LogWarning("  Já cadastrado (keyValue: \"{KeyValue}\")", _payload.KeyValue);
            _logger.LogInformation("  Account Type ID: {Id}", found.Id);
            return new SetupStepResult<AccountType>(found, WasCreated: false);
        }

        var created = await _api.CreateAccountTypeAsync(organizationId, ledgerId, _payload);
        _logger.LogInformation("  Criado — ID: {Id}", created.Id);
        return new SetupStepResult<AccountType>(created, WasCreated: true);
    }

    private async Task<PagedResult<AccountType>> FetchExistingAccountTypesAsync(
        string organizationId,
        string ledgerId)
    {
        try
        {
            return await _api.GetAccountTypesAsync(organizationId, ledgerId);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogInformation("  Nenhum account type encontrado (404).");
            return new PagedResult<AccountType>();
        }
    }
}
