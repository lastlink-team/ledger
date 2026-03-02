# Ledger Spike

Ferramenta de linha de comando em **.NET 8 / C#** para configurar e testar a plataforma [Lerian](https://lerian.net) via API REST.

Possui dois modos de execução:

| Modo | O que faz |
|------|-----------|
| `setup` | Cria toda a estrutura base do ledger (organização, ledger, asset, account type, operation routes e transaction routes) e gera um relatório em Markdown |
| `transaction` | Envia uma transação de teste para a API e gera um relatório com o payload enviado e a resposta |

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Acesso a uma instância Lerian (local, dev, sandbox ou produção)
- API Key de autenticação

---

## Configuração

### API Key

Preencha o campo `ApiKey` no arquivo `appsettings.json` (ou no arquivo do ambiente desejado):

```json
{
  "Ledger": {
    "OnboardingBaseUrl": "...",
    "TransactionBaseUrl": "...",
    "ApiKey": "SEU_TOKEN_AQUI"
  }
}
```

### Ambientes disponíveis

| Ambiente | Arquivo | URLs |
|----------|---------|------|
| `Local` | `appsettings.Local.json` | `http://localhost:3000/v1` |
| `Development` _(padrão)_ | `appsettings.Development.json` | `https://midaz-*.dev.llhub.net/v1` |
| `Sandbox` | `appsettings.Sandbox.json` | `https://ledger.sandbox.lerian.net/v1` |
| `Production` | `appsettings.Production.json` | `https://midaz-*.llhub.net/v1` |

O ambiente é selecionado pela variável `DOTNET_ENVIRONMENT`:

```bash
# Linux / macOS
export DOTNET_ENVIRONMENT=Sandbox

# Windows (PowerShell)
$env:DOTNET_ENVIRONMENT = "Sandbox"
```

---

## Execução

```bash
cd Lastlink.Ledger.Console

# Modo setup (padrão)
dotnet run

# Modo setup explícito
dotnet run -- setup

# Modo transaction
dotnet run -- transaction

# Combinando modo e ambiente
dotnet run -- setup --environment Sandbox
dotnet run -- transaction --environment Production
```

---

## Modo `setup`

Executa 6 etapas sequenciais, com idempotência em cada uma (não recria o que já existe):

```
Step 1 · Organização
Step 2 · Ledger
Step 3 · Asset
Step 4 · Account Type
Step 5 · Operation Routes  (N rotas)
Step 6 · Transaction Routes (N rotas)
Step 7 · Geração do relatório
```

### Dados de entrada

Os payloads ficam em `Lastlink.Ledger.Console/Data/`:

| Arquivo | Descrição |
|---------|-----------|
| `organization.json` | Dados da organização |
| `ledger.json` | Nome do ledger |
| `asset.json` | Ativo (ex.: BRL) |
| `account_type.json` | Tipo de conta com regras |
| `operation_routes.json` | Lista de operation routes |
| `transaction_routes.json` | Lista de transaction routes |

### Relatório gerado

`Lastlink.Ledger.Console/report/setup_report_{ambiente}.md`

Contém banner visual, tabela de resumo, detalhes de cada recurso criado com links clicáveis para a API e os JSONs completos em seções colapsáveis.

---

## Modo `transaction`

Envia uma transação de teste para `POST /organizations/{id}/ledgers/{id}/transactions/json`.

### Configuração prévia

Antes de executar, preencha `Lastlink.Ledger.Console/Data/transaction_test.json` com os IDs reais:

```json
{
  "organizationId": "SEU_ORG_ID",
  "ledgerId": "SEU_LEDGER_ID",
  "description": "PIX",
  "pending": false,
  "code": "TXN-2026-000001",
  "transactionDate": "2026-02-25T21:06:38Z",
  "send": {
    "asset": "BRL",
    "value": "1000",
    "source": {
      "from": [
        {
          "accountAlias": "@external/BRL",
          "balanceKey": "default",
          "amount": { "asset": "BRL", "value": "1000" },
          "description": "Debit pix"
        }
      ]
    },
    "distribute": {
      "to": [
        {
          "accountAlias": "customer-brl-1",
          "balanceKey": "default",
          "amount": { "asset": "BRL", "value": "1000" },
          "description": "Credit pix"
        }
      ]
    }
  }
}
```

### Relatório gerado

`Lastlink.Ledger.Console/report/transaction_report_{ambiente}.md`

Contém os parâmetros do teste, o resultado da API (ID, status, data de criação, link de detalhe) e os JSONs de request e response em seções colapsáveis.

---

## Estrutura do projeto

```
Ledger/
├── Lastlink.Ledger.Domain/
│   └── Models/                  # Modelos de resposta da API
│       ├── Organization.cs
│       ├── LedgerInfo.cs
│       ├── Asset.cs
│       ├── AccountType.cs
│       ├── OperationRoute.cs
│       ├── TransactionRoute.cs
│       └── Transaction.cs
│
├── Lastlink.Ledger.Application/
│   └── Application/
│       ├── Contracts/           # Interfaces Refit (HTTP)
│       ├── DTOs/                # Requests e commands de seed
│       ├── Interfaces/          # Contratos dos use cases
│       ├── UseCases/            # Implementações
│       └── Orchestrator/        # Orquestrador do fluxo setup
│
├── Lastlink.Ledger.Infrastructure/
│   ├── Configuration/           # LedgerSettings, SeedDataLoader
│   ├── Http/                    # AuthHeaderHandler
│   └── Reports/                 # MarkdownReportGenerator
│
└── Lastlink.Ledger.Console/
    ├── Data/                    # JSONs de seed e teste
    ├── DependencyInjection/     # Extensões de registro no DI
    ├── report/                  # Relatórios gerados (gitignore)
    ├── appsettings*.json
    └── Program.cs
```

### Dependências externas

| Pacote | Versão | Uso |
|--------|--------|-----|
| `Refit` | 8.x | HTTP client declarativo |
| `Refit.HttpClientFactory` | 8.x | Integração com `IHttpClientFactory` |
| `Microsoft.Extensions.Hosting` | 8.x | DI, configuração, logging |

---

## Relatórios

Os relatórios ficam em `Lastlink.Ledger.Console/report/` e são sobrescritos a cada execução bem-sucedida.
Recomenda-se adicionar esse diretório ao `.gitignore` se os dados forem sensíveis:

```gitignore
Lastlink.Ledger.Console/report/
```
