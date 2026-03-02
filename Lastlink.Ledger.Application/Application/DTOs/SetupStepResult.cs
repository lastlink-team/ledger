namespace Lastlink.Ledger.Application.DTOs;

public record SetupStepResult<T>(T Resource, bool WasCreated);
