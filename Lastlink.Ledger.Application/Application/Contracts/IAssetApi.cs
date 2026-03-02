using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface IAssetApi
{
    [Get("/organizations/{organizationId}/ledgers/{ledgerId}/assets")]
    Task<PagedResult<Asset>> GetAssetsAsync(
        string organizationId,
        string ledgerId,
        int page = 1,
        int limit = 100);

    [Post("/organizations/{organizationId}/ledgers/{ledgerId}/assets")]
    Task<Asset> CreateAssetAsync(
        string organizationId,
        string ledgerId,
        [Body] CreateAssetRequest request);
}
