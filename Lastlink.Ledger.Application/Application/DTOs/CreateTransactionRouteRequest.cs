namespace Lastlink.Ledger.Application.DTOs;

public record CreateTransactionRouteRequest(string Title, string? Description, List<string> OperationRoutes);
