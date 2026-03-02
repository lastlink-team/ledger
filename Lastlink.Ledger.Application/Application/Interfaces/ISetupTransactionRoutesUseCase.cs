using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ISetupTransactionRoutesUseCase
{
    Task<IReadOnlyList<SetupStepResult<TransactionRoute>>> ExecuteAsync(
        string organizationId,
        string ledgerId,
        IReadOnlyList<SetupStepResult<OperationRoute>> operationRouteResults);
}
