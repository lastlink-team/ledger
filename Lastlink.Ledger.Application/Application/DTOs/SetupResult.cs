using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.DTOs;

public record SetupResult(
    Organization Organization,
    bool OrgCreated,
    LedgerInfo Ledger,
    bool LedgerCreated,
    Asset Asset,
    bool AssetCreated,
    AccountType AccountType,
    bool AccountTypeCreated,
    IReadOnlyList<SetupStepResult<OperationRoute>> OperationRoutes,
    IReadOnlyList<SetupStepResult<TransactionRoute>> TransactionRoutes);
