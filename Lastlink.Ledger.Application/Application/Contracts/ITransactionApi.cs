using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface ITransactionApi
{
    [Post("/organizations/{organizationId}/ledgers/{ledgerId}/transactions/json")]
    Task<Transaction> CreateTransactionAsync(
        string organizationId,
        string ledgerId,
        [Body] CreateTransactionRequest request);
}
