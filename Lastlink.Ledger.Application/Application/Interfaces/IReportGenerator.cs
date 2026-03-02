using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.Interfaces;

public interface IReportGenerator
{
    Task GenerateAsync(SetupResult result, string environment);
    Task AppendTransactionAsync(Transaction result, CreateTransactionCommand command, string environment);
}
