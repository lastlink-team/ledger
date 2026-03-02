namespace Lastlink.Ledger.Domain.Models;

public record OperationRoute
{
    public string? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? OperationType { get; init; }
    public AccountRule? Account { get; init; }
    public string? Code { get; init; }
    public string? Description { get; init; }
    public Status? Status { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
