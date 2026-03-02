namespace Lastlink.Ledger.Domain.Models;

public record LedgerInfo
{
    public string? Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
