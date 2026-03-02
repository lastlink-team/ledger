using Lastlink.Ledger.Application.Contracts;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Lastlink.Ledger.Application.UseCases;

public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly ITransactionApi _api;
    private readonly CreateTransactionCommand _command;
    private readonly ILogger<CreateTransactionUseCase> _logger;

    public CreateTransactionUseCase(
        ITransactionApi api,
        CreateTransactionCommand command,
        ILogger<CreateTransactionUseCase> logger)
    {
        _api = api;
        _command = command;
        _logger = logger;
    }

    public async Task<Transaction> ExecuteAsync()
    {
        _logger.LogInformation("=== Transaction Test ===");
        _logger.LogInformation("  Organization: {OrgId}", _command.OrganizationId);
        _logger.LogInformation("  Ledger:        {LedgerId}", _command.LedgerId);
        _logger.LogInformation("  Code:          {Code}", _command.Code);

        var result = await _api.CreateTransactionAsync(
            _command.OrganizationId,
            _command.LedgerId,
            _command.ToRequest());

        _logger.LogInformation("  Criado — ID: {Id}", result.Id);
        _logger.LogInformation("=== Transação criada com sucesso ===");

        return result;
    }
}
