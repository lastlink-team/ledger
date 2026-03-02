using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lastlink.Ledger.Application.Orchestrator;

public class SetupOrchestrator : ISetupOrchestrator
{
    private readonly ISetupOrganizationUseCase _orgSetup;
    private readonly ISetupLedgerUseCase _ledgerSetup;
    private readonly ISetupAssetUseCase _assetSetup;
    private readonly ISetupAccountTypeUseCase _accountTypeSetup;
    private readonly ISetupOperationRoutesUseCase _operationRoutesSetup;
    private readonly ISetupTransactionRoutesUseCase _transactionRoutesSetup;
    private readonly IReportGenerator _reportGenerator;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<SetupOrchestrator> _logger;

    public SetupOrchestrator(
        ISetupOrganizationUseCase orgSetup,
        ISetupLedgerUseCase ledgerSetup,
        ISetupAssetUseCase assetSetup,
        ISetupAccountTypeUseCase accountTypeSetup,
        ISetupOperationRoutesUseCase operationRoutesSetup,
        ISetupTransactionRoutesUseCase transactionRoutesSetup,
        IReportGenerator reportGenerator,
        IHostEnvironment environment,
        ILogger<SetupOrchestrator> logger)
    {
        _orgSetup = orgSetup;
        _ledgerSetup = ledgerSetup;
        _assetSetup = assetSetup;
        _accountTypeSetup = accountTypeSetup;
        _operationRoutesSetup = operationRoutesSetup;
        _transactionRoutesSetup = transactionRoutesSetup;
        _reportGenerator = reportGenerator;
        _environment = environment;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogInformation("=== Ledger Setup ===");
        _logger.LogInformation("  Ambiente: {Environment}", _environment.EnvironmentName);

        var (org, orgCreated) = await _orgSetup.ExecuteAsync();
        var (ledger, ledgerCreated) = await _ledgerSetup.ExecuteAsync(org.Id!);
        var (asset, assetCreated) = await _assetSetup.ExecuteAsync(org.Id!, ledger.Id!);
        var (accountType, accountTypeCreated) = await _accountTypeSetup.ExecuteAsync(org.Id!, ledger.Id!);
        var operationRoutes = await _operationRoutesSetup.ExecuteAsync(org.Id!, ledger.Id!);
        var transactionRoutes = await _transactionRoutesSetup.ExecuteAsync(org.Id!, ledger.Id!, operationRoutes);

        _logger.LogInformation("── Step 7: Relatório ────────────────────────────────────────");

        var result = new SetupResult(
            org, orgCreated,
            ledger, ledgerCreated,
            asset, assetCreated,
            accountType, accountTypeCreated,
            operationRoutes,
            transactionRoutes);

        await _reportGenerator.GenerateAsync(result, _environment.EnvironmentName);

        _logger.LogInformation("=== Setup concluído ===");
        _logger.LogInformation("  Organization ID:      {OrgId}", org.Id);
        _logger.LogInformation("  Ledger ID:            {LedgerId}", ledger.Id);
        _logger.LogInformation("  Asset ID:             {AssetId}", asset.Id);
        _logger.LogInformation("  Account Type ID:      {AccountTypeId}", accountType.Id);
        _logger.LogInformation("  Operation Routes:     {Count} rotas configuradas", operationRoutes.Count);
        _logger.LogInformation("  Transaction Routes:   {Count} rotas configuradas", transactionRoutes.Count);
    }
}
