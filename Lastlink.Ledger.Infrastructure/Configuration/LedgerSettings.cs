namespace Lastlink.Ledger.Infrastructure.Configuration;

public class LedgerSettings
{
    public const string SectionName = "Ledger";

    public string OnboardingBaseUrl { get; set; } = string.Empty;
    public string TransactionBaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
