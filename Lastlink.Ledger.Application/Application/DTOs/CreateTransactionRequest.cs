namespace Lastlink.Ledger.Application.DTOs;

public record TransactionAmount(string Asset, string Value);

public record TransactionEntry(
    string AccountAlias,
    string BalanceKey,
    TransactionAmount Amount,
    string Description);

public record TransactionSource(List<TransactionEntry> From);

public record TransactionDistribute(List<TransactionEntry> To);

public record TransactionSend(
    string Asset,
    string Value,
    TransactionSource Source,
    TransactionDistribute Distribute);

public record CreateTransactionRequest(
    TransactionSend Send,
    string Description,
    bool Pending,
    string Code,
    string TransactionDate);
