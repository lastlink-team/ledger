using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ISetupOperationRoutesUseCase
{
    Task<IReadOnlyList<SetupStepResult<OperationRoute>>> ExecuteAsync(
        string organizationId,
        string ledgerId);
}
