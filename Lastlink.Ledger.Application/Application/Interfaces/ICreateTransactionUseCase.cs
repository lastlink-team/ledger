using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface ICreateTransactionUseCase
{
    Task<Transaction> ExecuteAsync();
}
