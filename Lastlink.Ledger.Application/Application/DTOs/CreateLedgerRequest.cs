using Lastlink.Ledger.Domain.Models;

namespace Lastlink.Ledger.Application.DTOs;

public record CreateLedgerRequest(string Name, Status? Status);
