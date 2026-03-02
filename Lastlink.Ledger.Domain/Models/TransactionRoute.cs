namespace Lastlink.Ledger.Domain.Models;

public record TransactionRoute
{
    public string? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public IReadOnlyList<string>? OperationRoutes { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
