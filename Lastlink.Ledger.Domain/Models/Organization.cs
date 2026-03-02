namespace Lastlink.Ledger.Domain.Models;

public record Organization
{
    public string? Id { get; init; }
    public string LegalName { get; init; } = string.Empty;
    public string LegalDocument { get; init; } = string.Empty;
    public string? DoingBusinessAs { get; init; }
    public Status? Status { get; init; }
    public Address? Address { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
