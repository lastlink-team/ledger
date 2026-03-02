using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface IOperationRouteApi
{
    [Get("/organizations/{organizationId}/ledgers/{ledgerId}/operation-routes")]
    Task<PagedResult<OperationRoute>> GetOperationRoutesAsync(
        string organizationId,
        string ledgerId,
        int page = 1,
        int limit = 100);

    [Post("/organizations/{organizationId}/ledgers/{ledgerId}/operation-routes")]
    Task<OperationRoute> CreateOperationRouteAsync(
        string organizationId,
        string ledgerId,
        [Body] CreateOperationRouteRequest request);
}
