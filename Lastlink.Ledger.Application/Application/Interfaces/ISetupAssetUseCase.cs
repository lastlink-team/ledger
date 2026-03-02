using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ISetupAssetUseCase
{
    Task<SetupStepResult<Asset>> ExecuteAsync(string organizationId, string ledgerId);
}
