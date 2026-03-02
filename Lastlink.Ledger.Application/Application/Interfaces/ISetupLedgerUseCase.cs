using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ISetupLedgerUseCase
{
    Task<SetupStepResult<LedgerInfo>> ExecuteAsync(string organizationId);
}
