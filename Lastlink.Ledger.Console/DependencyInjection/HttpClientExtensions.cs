using System.Text.Json;
using Lastlink.Ledger.Infrastructure.Configuration;
using Lastlink.Ledger.Application.Contracts;
using Lastlink.Ledger.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Lastlink.Ledger.Console.DependencyInjection;

internal static class HttpClientExtensions
{
    internal static IServiceCollection AddLedgerHttpClients(this IServiceCollection services)
    {
        services.AddTransient<AuthHeaderHandler>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
        };

        services.AddRefitClient<IOrganizationApi>(refitSettings)
            .ConfigureHttpClient(SetBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<ILedgerApi>(refitSettings)
            .ConfigureHttpClient(SetBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<IAssetApi>(refitSettings)
            .ConfigureHttpClient(SetBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<IAccountTypeApi>(refitSettings)
            .ConfigureHttpClient(SetBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<IOperationRouteApi>(refitSettings)
            .ConfigureHttpClient(SetTransactionBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<ITransactionRouteApi>(refitSettings)
            .ConfigureHttpClient(SetTransactionBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services.AddRefitClient<ITransactionApi>(refitSettings)
            .ConfigureHttpClient(SetTransactionBaseAddress)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }

    private static void SetBaseAddress(IServiceProvider sp, HttpClient client)
    {
        var settings = sp.GetRequiredService<IOptions<LedgerSettings>>().Value;
        client.BaseAddress = new Uri(settings.OnboardingBaseUrl);
    }

    private static void SetTransactionBaseAddress(IServiceProvider sp, HttpClient client)
    {
        var settings = sp.GetRequiredService<IOptions<LedgerSettings>>().Value;
        client.BaseAddress = new Uri(settings.TransactionBaseUrl);
    }
}
