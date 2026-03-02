using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.DTOs;

public record CreateOrganizationRequest(
    string LegalName,
    string LegalDocument,
    string? DoingBusinessAs,
    Status? Status,
    Address? Address);
