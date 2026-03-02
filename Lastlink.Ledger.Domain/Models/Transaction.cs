namespace Lastlink.Ledger.Domain.Models;

public record Transaction
{
    public string? Id { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
