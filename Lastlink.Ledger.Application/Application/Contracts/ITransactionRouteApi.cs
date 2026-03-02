using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface ITransactionRouteApi
{
    [Get("/organizations/{organizationId}/ledgers/{ledgerId}/transaction-routes")]
    Task<PagedResult<TransactionRoute>> GetTransactionRoutesAsync(
        string organizationId,
        string ledgerId,
        int page = 1,
        int limit = 100);

    [Post("/organizations/{organizationId}/ledgers/{ledgerId}/transaction-routes")]
    Task<TransactionRoute> CreateTransactionRouteAsync(
        string organizationId,
        string ledgerId,
        [Body] CreateTransactionRouteRequest request);
}
