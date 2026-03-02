using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface IAccountTypeApi
{
    [Get("/organizations/{organizationId}/ledgers/{ledgerId}/account-types")]
    Task<PagedResult<AccountType>> GetAccountTypesAsync(
        string organizationId,
        string ledgerId,
        int page = 1,
        int limit = 100);

    [Post("/organizations/{organizationId}/ledgers/{ledgerId}/account-types")]
    Task<AccountType> CreateAccountTypeAsync(
        string organizationId,
        string ledgerId,
        [Body] CreateAccountTypeRequest request);
}
