using System.Text.Json.Serialization;

namespace Lastlink.Ledger.Domain.Models;

[JsonConverter(typeof(AccountRuleConverter))]
public record AccountRule
{
    public string RuleType { get; init; } = string.Empty;
    public IReadOnlyList<string> ValidIf { get; init; } = [];
}
