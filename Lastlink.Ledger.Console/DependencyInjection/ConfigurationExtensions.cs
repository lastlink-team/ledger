using Lastlink.Ledger.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lastlink.Ledger.Console.DependencyInjection;

internal static class ConfigurationExtensions
{
    internal static IServiceCollection AddLedgerConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<LedgerSettings>(configuration.GetSection(LedgerSettings.SectionName));
        return services;
    }
}
