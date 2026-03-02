namespace Lastlink.Ledger.Application.DTOs;

public record CreateTransactionCommand(
    string OrganizationId,
    string LedgerId,
    TransactionSend Send,
    string Description,
    bool Pending,
    string Code,
    string TransactionDate)
{
    public CreateTransactionRequest ToRequest() =>
        new(Send, Description, Pending, Code, TransactionDate);
}
