<div align="center">

<svg width="780" height="110" viewBox="0 0 780 110" xmlns="http://www.w3.org/2000/svg">
  <defs>
    <linearGradient id="hg" x1="0" y1="0" x2="1" y2="0">
      <stop offset="0%" stop-color="#0f172a"/>
      <stop offset="100%" stop-color="#2563eb" stop-opacity="0.9"/>
    </linearGradient>
  </defs>
  <rect width="780" height="110" rx="10" fill="url(#hg)"/>
  <rect x="0" y="0" width="6" height="110" rx="3" fill="white" opacity="0.3"/>
  <text x="28" y="46" font-family="system-ui,sans-serif" font-size="24" font-weight="700" fill="white" letter-spacing="-0.5">Ledger Spike Report</text>
  <text x="28" y="72" font-family="system-ui,sans-serif" font-size="13" fill="rgba(255,255,255,0.6)">02 de março de 2026 · 18:59:10</text>
  <rect x="578" y="20" width="178" height="70" rx="8" fill="rgba(255,255,255,0.08)" stroke="rgba(255,255,255,0.15)" stroke-width="1"/>
  <text x="667" y="50" font-family="system-ui,sans-serif" font-size="9" fill="rgba(255,255,255,0.5)" text-anchor="middle" letter-spacing="3">AMBIENTE</text>
  <text x="667" y="74" font-family="system-ui,sans-serif" font-size="16" font-weight="600" fill="white" text-anchor="middle" letter-spacing="1">DEVELOPMENT</text>
</svg>

</div>

---

## Resumo

| Recurso | Identificador | ID | Status |
|---------|---------------|----|:------:|
| **Organização** | LASTLINK TECNOLOGIA S.A. | `019c9503…72eb` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| **Ledger** | Carteira Lastlink | `019c9503…47af` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| **Asset** | Brazilian Real — `BRL` | `019c9503…91a3` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| **Account Type** | Conta EC — `conta_ec_` | `019caf93…8144` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| **Operation Routes** | 8 rotas cadastradas | — | ![](https://img.shields.io/badge/ja+existiam-6b7280?style=flat-square) 8 |
| **Transaction Routes** | 4 rotas cadastradas | — | ![](https://img.shields.io/badge/ja+existiam-6b7280?style=flat-square) 4 |

---

## Organização

> `019c9503-d814-743c-929e-189e416272eb`

**`GET`** [`https://midaz-onboarding.dev.llhub.net/v1/organizations`](https://midaz-onboarding.dev.llhub.net/v1/organizations)

| Campo | Valor |
|-------|-------|
| **Razão Social** | LASTLINK TECNOLOGIA S.A. |
| **Nome Fantasia** | HorizonPay |
| **CNPJ** | `38220040000111` |
| **Situação** | ![](https://img.shields.io/badge/ACTIVE-22c55e?style=flat-square) |
| **Endereço** | AV GETULIO VARGAS 874 SALA 1405, Belo Horizonte — MG |
| **Criado em** | 25/02/2026 às 13:36:23 |

---

## Ledger

> `019c9503-da6e-7006-a169-4ca2755547af`

**`GET`** [`https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers`](https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers)

| Campo | Valor |
|-------|-------|
| **Nome** | Carteira Lastlink |
| **Situação** | ![](https://img.shields.io/badge/ACTIVE-22c55e?style=flat-square) |
| **Descrição** | Razão de produção padrão para operações financeiras |
| **Criado em** | 25/02/2026 às 13:36:23 |

---

## Asset

> `019c9503-db99-7f34-9699-0f37971291a3`

**`GET`** [`https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/assets`](https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/assets)

| Campo | Valor |
|-------|-------|
| **Nome** | Brazilian Real |
| **Código** | `BRL` |
| **Tipo** | `currency` |
| **Situação** | ![](https://img.shields.io/badge/ACTIVE-22c55e?style=flat-square) |
| **Descrição** | Primary fiat asset used for domestic settlement and customer balances |
| **Criado em** | 25/02/2026 às 13:36:23 |

---

## Account Type

> `019caf93-4304-7717-97c5-aad0e4488144`

**`GET`** [`https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/account-types`](https://midaz-onboarding.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/account-types)

| Campo | Valor |
|-------|-------|
| **Nome** | Conta EC |
| **Key** | `conta_ec_` |
| **Descrição** | Produtores, coprodutores e afiliados. Ou seja, qualquer usuário que recebe e saca |
| **Situação** | — |
| **Criado em** | 02/03/2026 às 17:23:09 |

---

## Operation Routes

**`GET`** [`https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/operation-routes`](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/operation-routes)

| Código | Descrição | Direção | Regra | Conta Alvo | ID | Status |
|--------|-----------|:-------:|:-----:|-----------|-----|:------:|
| `cashin_external_debito` | Cashin EC — debito na conta externa | ![](https://img.shields.io/badge/source-3b82f6?style=flat-square) | ![](https://img.shields.io/badge/alias-8b5cf6?style=flat-square) | `@external/BRL` | `019cb005…6322` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `cashin_ec_credito` | Cashin EC — credito na conta EC | ![](https://img.shields.io/badge/destination-f97316?style=flat-square) | ![](https://img.shields.io/badge/account__type-0ea5e9?style=flat-square) | `conta_ec` | `019cb037…babb` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `saque_ec_debito` | Saque EC — debito na conta EC | ![](https://img.shields.io/badge/source-3b82f6?style=flat-square) | ![](https://img.shields.io/badge/account__type-0ea5e9?style=flat-square) | `conta_ec_` | `019cb037…88d0` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `saque_external_credito` | Saque EC — credito na conta externa | ![](https://img.shields.io/badge/destination-f97316?style=flat-square) | ![](https://img.shields.io/badge/alias-8b5cf6?style=flat-square) | `@external/BRL` | `019cb037…669d` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `wallet_transfer_debito` | Transferencia — debito na conta EC | ![](https://img.shields.io/badge/source-3b82f6?style=flat-square) | ![](https://img.shields.io/badge/account__type-0ea5e9?style=flat-square) | `conta_ec_` | `019cb037…9a7a` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `wallet_transfer_credito` | Transferencia — credito na conta EC | ![](https://img.shields.io/badge/destination-f97316?style=flat-square) | ![](https://img.shields.io/badge/account__type-0ea5e9?style=flat-square) | `conta_ec_` | `019cb037…816e` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `estorno_ec_debito` | Estorno EC — debito na conta EC | ![](https://img.shields.io/badge/source-3b82f6?style=flat-square) | ![](https://img.shields.io/badge/account__type-0ea5e9?style=flat-square) | `conta_ec_` | `019cb037…ac46` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| `estorno_external_credito` | Estorno EC — credito na conta externa | ![](https://img.shields.io/badge/destination-f97316?style=flat-square) | ![](https://img.shields.io/badge/alias-8b5cf6?style=flat-square) | `@external/BRL` | `019cb037…6bad` | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |

---

## Transaction Routes

**`GET`** [`https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes`](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes)

| Título | Descrição | Rotas de Operação | ID | Detalhe | Status |
|--------|-----------|-------------------|-----|---------|:------:|
| Cashin EC | Entrada de saldo via PIX (Moving Pay) ou cartão/boleto (IUGU) liquidados | — | `019cb062…8fb1` | [**`GET`**](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes/019cb062-215e-7b0c-b233-e510b9a28fb1) | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| Saque EC | Saque confirmado — saída definitiva do ledger | — | `019cb062…e4ed` | [**`GET`**](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes/019cb062-3c3e-7c90-936e-bb68e64ae4ed) | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| Transferência entre Carteiras | Movimentação interna entre carteiras do mesmo usuário | — | `019cb062…7e68` | [**`GET`**](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes/019cb062-58f0-7df4-baee-23f2b0647e68) | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |
| Estorno EC | Estorno, cancelamento, reembolso, chargeback ou MED PIX | — | `019cb062…e5ab` | [**`GET`**](https://midaz-transaction.dev.llhub.net/v1/organizations/019c9503-d814-743c-929e-189e416272eb/ledgers/019c9503-da6e-7006-a169-4ca2755547af/transaction-routes/019cb062-598d-7dae-9dbd-8f5e2216e5ab) | ![](https://img.shields.io/badge/Ja+existia-6b7280?style=flat-square) |

---

## Payloads

<details>
<summary>Organização — JSON</summary>

```json
{
  "Id": "019c9503-d814-743c-929e-189e416272eb",
  "LegalName": "LASTLINK TECNOLOGIA S.A.",
  "LegalDocument": "38220040000111",
  "DoingBusinessAs": "HorizonPay",
  "Status": {
    "Code": "ACTIVE",
    "Description": "Com sede em BELO HORIZONTE, MG, possui 5 anos, 5 meses e 29 dias e foi fundada em 25/08/2020. A sua situação cadastral é ATIVA e sua principal atividade econômica é Desenvolvimento e licenciamento de programas de computador não-customizáveis."
  },
  "Address": {
    "Line1": "AV GETULIO VARGAS 874 SALA 1405",
    "ZipCode": "30112-021",
    "City": "Belo Horizonte",
    "State": "MG",
    "Country": "BR",
    "Line2": "Savassi"
  },
  "CreatedAt": "2026-02-25T13:36:23.06027Z",
  "UpdatedAt": "2026-02-25T13:36:23.06027Z"
}
```

</details>

<details>
<summary>Ledger — JSON</summary>

```json
{
  "Id": "019c9503-da6e-7006-a169-4ca2755547af",
  "Name": "Carteira Lastlink",
  "Status": {
    "Code": "ACTIVE",
    "Description": "Razão de produção padrão para operações financeiras"
  },
  "CreatedAt": "2026-02-25T13:36:23.661996Z",
  "UpdatedAt": "2026-02-25T13:36:23.661996Z"
}
```

</details>

<details>
<summary>Asset — JSON</summary>

```json
{
  "Id": "019c9503-db99-7f34-9699-0f37971291a3",
  "Name": "Brazilian Real",
  "Code": "BRL",
  "Type": "currency",
  "Status": {
    "Code": "ACTIVE",
    "Description": "Primary fiat asset used for domestic settlement and customer balances"
  },
  "CreatedAt": "2026-02-25T13:36:23.961988Z",
  "UpdatedAt": "2026-02-25T13:36:23.961988Z"
}
```

</details>

<details>
<summary>Account Type — JSON</summary>

```json
{
  "Id": "019caf93-4304-7717-97c5-aad0e4488144",
  "Name": "Conta EC",
  "KeyValue": "conta_ec_",
  "Description": "Produtores, coprodutores e afiliados. Ou seja, qualquer usuário que recebe e saca",
  "Status": null,
  "CreatedAt": "2026-03-02T17:23:09.70046Z",
  "UpdatedAt": "2026-03-02T17:23:09.70046Z"
}
```

</details>

<details>
<summary>Operation Routes — JSON</summary>

```json
[
  {
    "Id": "019cb005-c29c-72ca-bc9b-c239e66d6322",
    "Title": "Cashin External Debito",
    "OperationType": "source",
    "Account": {
      "ruleType": "alias",
      "validIf": "@external/BRL"
    },
    "Code": "cashin_external_debito",
    "Description": "Cashin EC — debito na conta externa",
    "Status": null,
    "CreatedAt": "2026-03-02T19:28:13.468178Z",
    "UpdatedAt": "2026-03-02T19:28:13.468178Z"
  },
  {
    "Id": "019cb037-2d21-7855-bc97-197fc857babb",
    "Title": "Cashin EC Credito",
    "OperationType": "destination",
    "Account": {
      "ruleType": "account_type",
      "validIf": [
        "conta_ec"
      ]
    },
    "Code": "cashin_ec_credito",
    "Description": "Cashin EC — credito na conta EC",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:12.001542Z",
    "UpdatedAt": "2026-03-02T20:22:12.001542Z"
  },
  {
    "Id": "019cb037-4864-7ad8-9a27-4fc1bda688d0",
    "Title": "Saque EC Debito",
    "OperationType": "source",
    "Account": {
      "ruleType": "account_type",
      "validIf": [
        "conta_ec_"
      ]
    },
    "Code": "saque_ec_debito",
    "Description": "Saque EC — debito na conta EC",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:18.980706Z",
    "UpdatedAt": "2026-03-02T20:22:18.980706Z"
  },
  {
    "Id": "019cb037-4903-784c-a30c-78786b6d669d",
    "Title": "Saque External Credito",
    "OperationType": "destination",
    "Account": {
      "ruleType": "alias",
      "validIf": "@external/BRL"
    },
    "Code": "saque_external_credito",
    "Description": "Saque EC — credito na conta externa",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:19.139539Z",
    "UpdatedAt": "2026-03-02T20:22:19.139539Z"
  },
  {
    "Id": "019cb037-49a4-7e75-8f4a-2bf32e2a9a7a",
    "Title": "Wallet Transfer Debito",
    "OperationType": "source",
    "Account": {
      "ruleType": "account_type",
      "validIf": [
        "conta_ec_"
      ]
    },
    "Code": "wallet_transfer_debito",
    "Description": "Transferencia — debito na conta EC",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:19.300943Z",
    "UpdatedAt": "2026-03-02T20:22:19.300943Z"
  },
  {
    "Id": "019cb037-4a43-75d2-b2a1-d7cfd3c7816e",
    "Title": "Wallet Transfer Credito",
    "OperationType": "destination",
    "Account": {
      "ruleType": "account_type",
      "validIf": [
        "conta_ec_"
      ]
    },
    "Code": "wallet_transfer_credito",
    "Description": "Transferencia — credito na conta EC",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:19.459377Z",
    "UpdatedAt": "2026-03-02T20:22:19.459377Z"
  },
  {
    "Id": "019cb037-4ae0-7621-9e7f-2c0a5552ac46",
    "Title": "Estorno EC Debito",
    "OperationType": "source",
    "Account": {
      "ruleType": "account_type",
      "validIf": [
        "conta_ec_"
      ]
    },
    "Code": "estorno_ec_debito",
    "Description": "Estorno EC — debito na conta EC",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:19.616398Z",
    "UpdatedAt": "2026-03-02T20:22:19.616398Z"
  },
  {
    "Id": "019cb037-4b7b-7b67-87a7-d085a1906bad",
    "Title": "Estorno External Credito",
    "OperationType": "destination",
    "Account": {
      "ruleType": "alias",
      "validIf": "@external/BRL"
    },
    "Code": "estorno_external_credito",
    "Description": "Estorno EC — credito na conta externa",
    "Status": null,
    "CreatedAt": "2026-03-02T20:22:19.771743Z",
    "UpdatedAt": "2026-03-02T20:22:19.771743Z"
  }
]
```

</details>

<details>
<summary>Transaction Routes — JSON</summary>

```json
[
  {
    "Id": "019cb062-215e-7b0c-b233-e510b9a28fb1",
    "Title": "Cashin EC",
    "Description": "Entrada de saldo via PIX (Moving Pay) ou cartão/boleto (IUGU) liquidados",
    "OperationRoutes": null,
    "Status": null,
    "CreatedAt": "2026-03-02T21:09:07.03872Z",
    "UpdatedAt": "2026-03-02T21:09:07.03872Z"
  },
  {
    "Id": "019cb062-3c3e-7c90-936e-bb68e64ae4ed",
    "Title": "Saque EC",
    "Description": "Saque confirmado — saída definitiva do ledger",
    "OperationRoutes": null,
    "Status": null,
    "CreatedAt": "2026-03-02T21:09:13.918818Z",
    "UpdatedAt": "2026-03-02T21:09:13.918818Z"
  },
  {
    "Id": "019cb062-58f0-7df4-baee-23f2b0647e68",
    "Title": "Transferência entre Carteiras",
    "Description": "Movimentação interna entre carteiras do mesmo usuário",
    "OperationRoutes": null,
    "Status": null,
    "CreatedAt": "2026-03-02T21:09:21.26491Z",
    "UpdatedAt": "2026-03-02T21:09:21.26491Z"
  },
  {
    "Id": "019cb062-598d-7dae-9dbd-8f5e2216e5ab",
    "Title": "Estorno EC",
    "Description": "Estorno, cancelamento, reembolso, chargeback ou MED PIX",
    "OperationRoutes": null,
    "Status": null,
    "CreatedAt": "2026-03-02T21:09:21.421893Z",
    "UpdatedAt": "2026-03-02T21:09:21.421893Z"
  }
]
```

</details>