using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Domain.Models;
using Lastlink.Ledger.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lastlink.Ledger.Infrastructure.Reports;

public class MarkdownReportGenerator : IReportGenerator
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<MarkdownReportGenerator> _logger;
    private readonly LedgerSettings _settings;

    private static readonly JsonSerializerOptions PrettyJson = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public MarkdownReportGenerator(IHostEnvironment environment, ILogger<MarkdownReportGenerator> logger, IOptions<LedgerSettings> settings)
    {
        _environment = environment;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task GenerateAsync(SetupResult result, string environment)
    {
        var content = BuildMarkdown(result, environment, _settings.OnboardingBaseUrl, _settings.TransactionBaseUrl);

        var reportDir = Path.Combine(_environment.ContentRootPath, "report");
        Directory.CreateDirectory(reportDir);

        var reportPath = Path.Combine(reportDir, $"setup_report_{environment.ToLower()}.md");
        await File.WriteAllTextAsync(reportPath, content, Encoding.UTF8);

        _logger.LogInformation("  Relatório gerado: {Path}", reportPath);
    }

    public async Task AppendTransactionAsync(Transaction result, CreateTransactionCommand command, string environment)
    {
        var content = BuildTransactionMarkdown(result, command, environment, _settings.TransactionBaseUrl);

        var reportDir = Path.Combine(_environment.ContentRootPath, "report");
        Directory.CreateDirectory(reportDir);

        var reportPath = Path.Combine(reportDir, $"transaction_report_{environment.ToLower()}.md");
        await File.WriteAllTextAsync(reportPath, content, Encoding.UTF8);

        _logger.LogInformation("  Relatório gerado: {Path}", reportPath);
    }

    private static string BuildMarkdown(SetupResult r, string environment, string onboardingBaseUrl, string transactionBaseUrl)
    {
        var culture = new System.Globalization.CultureInfo("pt-BR");
        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");
        var date = now.ToString("dd 'de' MMMM 'de' yyyy", culture);
        var time = now.ToString("HH:mm:ss");

        static string ShortId(string? id) => id is { Length: > 12 } ? $"`{id[..8]}…{id[^4..]}`" : $"`{id}`";
        static string FormatDate(DateTime? dt) => dt?.ToString("dd/MM/yyyy 'às' HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")) ?? "—";

        var orgId     = r.Organization.Id;
        var ledgerId  = r.Ledger.Id;

        var urlOrg               = $"{onboardingBaseUrl}/organizations";
        var urlLedgers           = $"{onboardingBaseUrl}/organizations/{orgId}/ledgers";
        var urlAssets            = $"{onboardingBaseUrl}/organizations/{orgId}/ledgers/{ledgerId}/assets";
        var urlAccountTypes      = $"{onboardingBaseUrl}/organizations/{orgId}/ledgers/{ledgerId}/account-types";
        var urlRoutes            = $"{transactionBaseUrl}/organizations/{orgId}/ledgers/{ledgerId}/operation-routes";
        var urlTransactionRoutes = $"{transactionBaseUrl}/organizations/{orgId}/ledgers/{ledgerId}/transaction-routes";

        var idToCode = r.OperationRoutes
            .Where(x => x.Resource.Id is not null && x.Resource.Code is not null)
            .ToDictionary(x => x.Resource.Id!, x => x.Resource.Code!);

        var routeCreated = r.OperationRoutes.Count(x => x.WasCreated);
        var routeExisting = r.OperationRoutes.Count(x => !x.WasCreated);
        var routeSummary = (routeCreated, routeExisting) switch
        {
            (> 0, > 0) => $"{Badge("criadas", "22c55e")} {routeCreated} &nbsp; {Badge("ja+existiam", "6b7280")} {routeExisting}",
            (> 0, _)   => $"{Badge("criadas", "22c55e")} {routeCreated}",
            _          => $"{Badge("ja+existiam", "6b7280")} {routeExisting}"
        };

        var txCreated = r.TransactionRoutes.Count(x => x.WasCreated);
        var txExisting = r.TransactionRoutes.Count(x => !x.WasCreated);
        var txSummary = (txCreated, txExisting) switch
        {
            (> 0, > 0) => $"{Badge("criadas", "22c55e")} {txCreated} &nbsp; {Badge("ja+existiam", "6b7280")} {txExisting}",
            (> 0, _)   => $"{Badge("criadas", "22c55e")} {txCreated}",
            _          => $"{Badge("ja+existiam", "6b7280")} {txExisting}"
        };

        return $"""
            {BuildBanner(environment, date, time)}

            ---

            ## Resumo

            | Recurso | Identificador | ID | Status |
            |---------|---------------|----|:------:|
            | **Organização** | {r.Organization.LegalName} | {ShortId(r.Organization.Id)} | {StatusBadge(r.OrgCreated)} |
            | **Ledger** | {r.Ledger.Name} | {ShortId(r.Ledger.Id)} | {StatusBadge(r.LedgerCreated)} |
            | **Asset** | {r.Asset.Name} — `{r.Asset.Code}` | {ShortId(r.Asset.Id)} | {StatusBadge(r.AssetCreated)} |
            | **Account Type** | {r.AccountType.Name} — `{r.AccountType.KeyValue}` | {ShortId(r.AccountType.Id)} | {StatusBadge(r.AccountTypeCreated)} |
            | **Operation Routes** | {r.OperationRoutes.Count} rotas cadastradas | — | {routeSummary} |
            | **Transaction Routes** | {r.TransactionRoutes.Count} rotas cadastradas | — | {txSummary} |

            ---

            ## Organização

            > `{r.Organization.Id}`

            **`GET`** [`{urlOrg}`]({urlOrg})

            | Campo | Valor |
            |-------|-------|
            | **Razão Social** | {r.Organization.LegalName} |
            | **Nome Fantasia** | {r.Organization.DoingBusinessAs} |
            | **CNPJ** | `{r.Organization.LegalDocument}` |
            | **Situação** | {SituacaoBadge(r.Organization.Status?.Code)} |
            | **Endereço** | {FormatAddress(r.Organization.Address)} |
            | **Criado em** | {FormatDate(r.Organization.CreatedAt)} |

            ---

            ## Ledger

            > `{r.Ledger.Id}`

            **`GET`** [`{urlLedgers}`]({urlLedgers})

            | Campo | Valor |
            |-------|-------|
            | **Nome** | {r.Ledger.Name} |
            | **Situação** | {SituacaoBadge(r.Ledger.Status?.Code)} |
            | **Descrição** | {r.Ledger.Status?.Description} |
            | **Criado em** | {FormatDate(r.Ledger.CreatedAt)} |

            ---

            ## Asset

            > `{r.Asset.Id}`

            **`GET`** [`{urlAssets}`]({urlAssets})

            | Campo | Valor |
            |-------|-------|
            | **Nome** | {r.Asset.Name} |
            | **Código** | `{r.Asset.Code}` |
            | **Tipo** | `{r.Asset.Type}` |
            | **Situação** | {SituacaoBadge(r.Asset.Status?.Code)} |
            | **Descrição** | {r.Asset.Status?.Description} |
            | **Criado em** | {FormatDate(r.Asset.CreatedAt)} |

            ---

            ## Account Type

            > `{r.AccountType.Id}`

            **`GET`** [`{urlAccountTypes}`]({urlAccountTypes})

            | Campo | Valor |
            |-------|-------|
            | **Nome** | {r.AccountType.Name} |
            | **Key** | `{r.AccountType.KeyValue}` |
            | **Descrição** | {r.AccountType.Description} |
            | **Situação** | {SituacaoBadge(r.AccountType.Status?.Code)} |
            | **Criado em** | {FormatDate(r.AccountType.CreatedAt)} |

            ---

            ## Operation Routes

            **`GET`** [`{urlRoutes}`]({urlRoutes})

            | Código | Descrição | Direção | Regra | Conta Alvo | ID | Status |
            |--------|-----------|:-------:|:-----:|-----------|-----|:------:|
            {BuildRouteRows(r.OperationRoutes)}

            ---

            ## Transaction Routes

            **`GET`** [`{urlTransactionRoutes}`]({urlTransactionRoutes})

            | Título | Descrição | Rotas de Operação | ID | Detalhe | Status |
            |--------|-----------|-------------------|-----|---------|:------:|
            {BuildTransactionRouteRows(r.TransactionRoutes, idToCode, urlTransactionRoutes)}

            ---

            ## Payloads

            <details>
            <summary>Organização — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.Organization, PrettyJson)}
            ```

            </details>

            <details>
            <summary>Ledger — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.Ledger, PrettyJson)}
            ```

            </details>

            <details>
            <summary>Asset — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.Asset, PrettyJson)}
            ```

            </details>

            <details>
            <summary>Account Type — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.AccountType, PrettyJson)}
            ```

            </details>

            <details>
            <summary>Operation Routes — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.OperationRoutes.Select(x => x.Resource).ToList(), PrettyJson)}
            ```

            </details>

            <details>
            <summary>Transaction Routes — JSON</summary>

            ```json
            {JsonSerializer.Serialize(r.TransactionRoutes.Select(x => x.Resource).ToList(), PrettyJson)}
            ```

            </details>
            """;
    }

    // ── Transaction report ────────────────────────────────────────────────────

    private static string BuildTransactionMarkdown(
        Transaction result,
        CreateTransactionCommand command,
        string environment,
        string transactionBaseUrl)
    {
        var culture = new System.Globalization.CultureInfo("pt-BR");
        var now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "E. South America Standard Time");
        var date = now.ToString("dd 'de' MMMM 'de' yyyy", culture);
        var time = now.ToString("HH:mm:ss");
        static string FormatDate(DateTime? dt) => dt?.ToString("dd/MM/yyyy 'às' HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")) ?? "—";

        var detailUrl = result.Id is not null
            ? $"{transactionBaseUrl}/organizations/{command.OrganizationId}/ledgers/{command.LedgerId}/transactions/{result.Id}"
            : null;

        var requestJson  = JsonSerializer.Serialize(command.ToRequest(), PrettyJson);
        var responseJson = JsonSerializer.Serialize(result, PrettyJson);

        return $"""
            {BuildBanner(environment, date, time)}

            ---

            ## Teste de Transação

            | Campo | Valor |
            |-------|-------|
            | **Organization ID** | `{command.OrganizationId}` |
            | **Ledger ID** | `{command.LedgerId}` |
            | **Código** | `{command.Code}` |
            | **Descrição** | {command.Description} |
            | **Data da Transação** | `{command.TransactionDate}` |
            | **Pending** | `{command.Pending.ToString().ToLower()}` |

            ---

            ## Resultado

            | Campo | Valor |
            |-------|-------|
            | **ID** | `{result.Id ?? "—"}` |
            | **Código** | `{result.Code ?? "—"}` |
            | **Situação** | {SituacaoBadge(result.Status?.Code)} |
            | **Criado em** | {FormatDate(result.CreatedAt)} |

            {(detailUrl is not null ? $"**`GET`** [`{detailUrl}`]({detailUrl})" : "")}

            ---

            ## Payload

            <details>
            <summary>Request — JSON</summary>

            ```json
            {requestJson}
            ```

            </details>

            <details>
            <summary>Response — JSON</summary>

            ```json
            {responseJson}
            ```

            </details>
            """;
    }

    // ── Banner SVG inline (sem dependência de internet) ──────────────────────

    private static string BuildBanner(string environment, string date, string time)
    {
        var (color, label) = environment.ToLower() switch
        {
            "production" => ("dc2626", "PRODUCTION"),
            "sandbox"    => ("d97706", "SANDBOX"),
            "local"      => ("7c3aed", "LOCAL"),
            _            => ("2563eb", "DEVELOPMENT")
        };

        // SVG com string concatenation para evitar conflito com url(#g) no raw literal
        return
            "<div align=\"center\">\n\n" +
            "<svg width=\"780\" height=\"110\" viewBox=\"0 0 780 110\" xmlns=\"http://www.w3.org/2000/svg\">\n" +
            "  <defs>\n" +
            "    <linearGradient id=\"hg\" x1=\"0\" y1=\"0\" x2=\"1\" y2=\"0\">\n" +
            "      <stop offset=\"0%\" stop-color=\"#0f172a\"/>\n" +
            $"      <stop offset=\"100%\" stop-color=\"#{color}\" stop-opacity=\"0.9\"/>\n" +
            "    </linearGradient>\n" +
            "  </defs>\n" +
            "  <rect width=\"780\" height=\"110\" rx=\"10\" fill=\"url(#hg)\"/>\n" +
            "  <rect x=\"0\" y=\"0\" width=\"6\" height=\"110\" rx=\"3\" fill=\"white\" opacity=\"0.3\"/>\n" +
            "  <text x=\"28\" y=\"46\" font-family=\"system-ui,sans-serif\" font-size=\"24\" font-weight=\"700\" fill=\"white\" letter-spacing=\"-0.5\">Ledger Spike Report</text>\n" +
            $"  <text x=\"28\" y=\"72\" font-family=\"system-ui,sans-serif\" font-size=\"13\" fill=\"rgba(255,255,255,0.6)\">{date} · {time}</text>\n" +
            "  <rect x=\"578\" y=\"20\" width=\"178\" height=\"70\" rx=\"8\" fill=\"rgba(255,255,255,0.08)\" stroke=\"rgba(255,255,255,0.15)\" stroke-width=\"1\"/>\n" +
            "  <text x=\"667\" y=\"50\" font-family=\"system-ui,sans-serif\" font-size=\"9\" fill=\"rgba(255,255,255,0.5)\" text-anchor=\"middle\" letter-spacing=\"3\">AMBIENTE</text>\n" +
            $"  <text x=\"667\" y=\"74\" font-family=\"system-ui,sans-serif\" font-size=\"16\" font-weight=\"600\" fill=\"white\" text-anchor=\"middle\" letter-spacing=\"1\">{label}</text>\n" +
            "</svg>\n\n" +
            "</div>";
    }

    // ── Badges via shields.io ─────────────────────────────────────────────────

    private static string Badge(string label, string color) =>
        $"![](https://img.shields.io/badge/{label}-{color}?style=flat-square)";

    private static string StatusBadge(bool created) => created
        ? Badge("Criado", "22c55e")
        : Badge("Ja+existia", "6b7280");

    private static string SituacaoBadge(string? code) => code switch
    {
        "ACTIVE"   => Badge("ACTIVE", "22c55e"),
        "INACTIVE" => Badge("INACTIVE", "ef4444"),
        null or "" => "—",
        _          => Badge(code, "6b7280")
    };

    private static string DirectionBadge(string? direction) => direction switch
    {
        "source"      => Badge("source", "3b82f6"),
        "destination" => Badge("destination", "f97316"),
        _             => $"`{direction}`"
    };

    private static string RuleBadge(string? rule) => rule switch
    {
        "alias"        => Badge("alias", "8b5cf6"),
        "account_type" => Badge("account__type", "0ea5e9"),
        _              => $"`{rule}`"
    };

    // ── Tabela de rotas ───────────────────────────────────────────────────────

    private static string BuildRouteRows(IReadOnlyList<SetupStepResult<OperationRoute>> routes)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < routes.Count; i++)
        {
            var item = routes[i];
            var route = item.Resource;
            var validIf = route.Account?.ValidIf is { } v
                ? string.Join(", ", v.Select(x => $"`{x}`"))
                : "—";
            var shortId = route.Id is { Length: > 12 } id
                ? $"`{id[..8]}…{id[^4..]}`"
                : $"`{route.Id}`";
            sb.Append($"| `{route.Code}` | {route.Description} | {DirectionBadge(route.OperationType)} | {RuleBadge(route.Account?.RuleType)} | {validIf} | {shortId} | {StatusBadge(item.WasCreated)} |");
            if (i < routes.Count - 1) sb.AppendLine();
        }
        return sb.ToString();
    }

    private static string BuildTransactionRouteRows(
        IReadOnlyList<SetupStepResult<TransactionRoute>> routes,
        Dictionary<string, string> idToCode,
        string baseUrl)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < routes.Count; i++)
        {
            var item = routes[i];
            var route = item.Resource;
            var shortId = route.Id is { Length: > 12 } id
                ? $"`{id[..8]}…{id[^4..]}`"
                : $"`{route.Id}`";
            var opCodes = route.OperationRoutes is { Count: > 0 }
                ? string.Join(", ", route.OperationRoutes.Select(rid =>
                    idToCode.TryGetValue(rid, out var code) ? $"`{code}`" : $"`{rid[..Math.Min(8, rid.Length)]}…`"))
                : "—";
            var detailUrl = route.Id is not null ? $"[**`GET`**]({baseUrl}/{route.Id})" : "—";
            sb.Append($"| {route.Title} | {route.Description} | {opCodes} | {shortId} | {detailUrl} | {StatusBadge(item.WasCreated)} |");
            if (i < routes.Count - 1) sb.AppendLine();
        }
        return sb.ToString();
    }

    private static string FormatAddress(Address? address)
    {
        if (address is null) return "—";
        return $"{address.Line1}, {address.City} — {address.State}";
    }
}
