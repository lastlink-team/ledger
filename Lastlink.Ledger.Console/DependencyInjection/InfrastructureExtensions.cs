using Lastlink.Ledger.Application.Interfaces;
using Lastlink.Ledger.Infrastructure.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Lastlink.Ledger.Console.DependencyInjection;

internal static class InfrastructureExtensions
{
    internal static IServiceCollection AddLedgerInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IReportGenerator, MarkdownReportGenerator>();
        return services;
    }
}
