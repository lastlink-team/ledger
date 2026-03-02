using Lastlink.Ledger.Console.DependencyInjection;
using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Extract mode before passing args to host builder
const string transactionMode = "transaction";
var mode = args.Contains(transactionMode, StringComparer.OrdinalIgnoreCase)
    ? transactionMode
    : "setup";
var hostArgs = args.Where(a => !a.Equals(transactionMode, StringComparison.OrdinalIgnoreCase)).ToArray();

var builder = Host.CreateApplicationBuilder(hostArgs);

builder.Services
    .AddLedgerConfiguration(builder.Configuration)
    .AddLedgerSeedData()
    .AddLedgerHttpClients()
    .AddLedgerApplication()
    .AddLedgerInfrastructure();

var host = builder.Build();

try
{
    if (mode == transactionMode)
    {
        var useCase  = host.Services.GetRequiredService<ICreateTransactionUseCase>();
        var command  = host.Services.GetRequiredService<CreateTransactionCommand>();
        var report   = host.Services.GetRequiredService<IReportGenerator>();
        var envName  = host.Services.GetRequiredService<IHostEnvironment>().EnvironmentName;

        var result = await useCase.ExecuteAsync();
        await report.AppendTransactionAsync(result, command, envName);
    }
    else
    {
        var orchestrator = host.Services.GetRequiredService<ISetupOrchestrator>();
        await orchestrator.RunAsync();
    }
    return 0;
}
catch (Refit.ApiException ex)
{
    var error = await ex.GetContentAsAsync<ApiErrorResponse>();

    if (error?.Code == "0008")
    {
        Console.Error.WriteLine("\nAção não permitida neste ambiente.");
        Console.Error.WriteLine($"   {error.Message}");
    }
    else
    {
        Console.Error.WriteLine($"\nErro na API ({(int)ex.StatusCode}): {ex.Message}");
    }

    return 1;
}
catch (HttpRequestException ex)
{
    Console.Error.WriteLine("\nConexão recusada — verifique se o ambiente está no ar.");
    Console.Error.WriteLine($"   {ex.Message}");
    return 1;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nErro fatal: {ex.Message}");
    return 1;
}

internal record ApiErrorResponse(string? Code, string? Message);
