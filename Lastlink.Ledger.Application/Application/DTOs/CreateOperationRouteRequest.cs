using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.DTOs;

public record CreateOperationRouteRequest(
    string Title,
    string OperationType,
    AccountRule Account,
    string Code,
    string? Description);
