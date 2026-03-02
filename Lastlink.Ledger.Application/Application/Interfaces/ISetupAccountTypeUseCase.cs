using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ISetupAccountTypeUseCase
{
    Task<SetupStepResult<AccountType>> ExecuteAsync(string organizationId, string ledgerId);
}
