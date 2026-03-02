namespace Lastlink.Ledger.Domain.Models;

public record AccountType
{
    public string? Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? KeyValue { get; init; }
    public string? Description { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
