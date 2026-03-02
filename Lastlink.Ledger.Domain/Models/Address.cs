namespace Lastlink.Ledger.Domain.Models;

public record Address(
    string Line1,
    string ZipCode,
    string City,
    string State,
    string Country,
    string? Line2 = null);
