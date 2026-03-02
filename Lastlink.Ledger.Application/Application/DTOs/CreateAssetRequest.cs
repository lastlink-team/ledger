using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.DTOs;

public record CreateAssetRequest(string Name, string Code, string? Type, Status? Status);
