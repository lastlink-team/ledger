using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Application.Orchestrator;
using Lastlink.Ledger.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Lastlink.Ledger.Console.DependencyInjection;

internal static class ApplicationExtensions
{
    internal static IServiceCollection AddLedgerApplication(this IServiceCollection services)
    {
        services.AddTransient<ISetupOrganizationUseCase, SetupOrganizationUseCase>();
        services.AddTransient<ISetupLedgerUseCase, SetupLedgerUseCase>();
        services.AddTransient<ISetupAssetUseCase, SetupAssetUseCase>();
        services.AddTransient<ISetupAccountTypeUseCase, SetupAccountTypeUseCase>();
        services.AddTransient<ISetupOperationRoutesUseCase, SetupOperationRoutesUseCase>();
        services.AddTransient<ISetupTransactionRoutesUseCase, SetupTransactionRoutesUseCase>();
        services.AddTransient<ISetupOrchestrator, SetupOrchestrator>();
        services.AddTransient<ICreateTransactionUseCase, CreateTransactionUseCase>();
        return services;
    }
}
