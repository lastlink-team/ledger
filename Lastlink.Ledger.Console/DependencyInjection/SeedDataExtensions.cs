using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lastlink.Ledger.Console.DependencyInjection;

internal static class SeedDataExtensions
{
    internal static IServiceCollection AddLedgerSeedData(this IServiceCollection services)
    {
        services.AddSingleton<SeedDataLoader>();

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<CreateOrganizationRequest>("organization.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<CreateLedgerRequest>("ledger.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<CreateAssetRequest>("asset.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<CreateAccountTypeRequest>("account_type.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<List<CreateOperationRouteRequest>>("operation_routes.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<List<CreateTransactionRouteRequest>>("transaction_routes.json"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<SeedDataLoader>().Load<CreateTransactionCommand>("transaction_test.json"));

        return services;
    }
}
