using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface ILedgerApi
{
    [Get("/organizations/{organizationId}/ledgers")]
    Task<PagedResult<LedgerInfo>> GetLedgersAsync(
        string organizationId,
        int page = 1,
        int limit = 100);

    [Post("/organizations/{organizationId}/ledgers")]
    Task<LedgerInfo> CreateLedgerAsync(string organizationId, [Body] CreateLedgerRequest request);
}
