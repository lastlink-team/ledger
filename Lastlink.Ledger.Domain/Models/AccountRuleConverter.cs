using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lastlink.Ledger.Domain.Models;

/// <summary>
/// Serializa/deserializa AccountRule respeitando a regra da API:
///   ruleType == "alias"        → validIf: string
///   ruleType == "account_type" → validIf: array de strings
/// </summary>
public class AccountRuleConverter : JsonConverter<AccountRule>
{
    public override AccountRule Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        string ruleType = string.Empty;
        IReadOnlyList<string> validIf = [];

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;

            var propName = reader.GetString();
            reader.Read();

            if (propName?.Equals("ruleType", StringComparison.OrdinalIgnoreCase) == true)
            {
                ruleType = reader.GetString() ?? string.Empty;
            }
            else if (propName?.Equals("validIf", StringComparison.OrdinalIgnoreCase) == true)
            {
                validIf = reader.TokenType switch
                {
                    JsonTokenType.String => [reader.GetString()!],
                    JsonTokenType.StartArray => ReadArray(ref reader),
                    _ => []
                };
            }
        }

        return new AccountRule { RuleType = ruleType, ValidIf = validIf };
    }

    public override void Write(
        Utf8JsonWriter writer,
        AccountRule value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("ruleType", value.RuleType);

        if (string.Equals(value.RuleType, "alias", StringComparison.OrdinalIgnoreCase))
        {
            // alias → string simples
            writer.WriteString("validIf", value.ValidIf.FirstOrDefault() ?? string.Empty);
        }
        else
        {
            // account_type (e outros) → array obrigatório
            writer.WriteStartArray("validIf");
            foreach (var item in value.ValidIf)
                writer.WriteStringValue(item);
            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }

    private static List<string> ReadArray(ref Utf8JsonReader reader)
    {
        var list = new List<string>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            list.Add(reader.GetString()!);
        return list;
    }
}
