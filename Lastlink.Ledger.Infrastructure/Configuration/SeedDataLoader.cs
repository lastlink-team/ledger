using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace Lastlink.Ledger.Infrastructure.Configuration;

public class SeedDataLoader
{
    private readonly string _dataPath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public SeedDataLoader(IHostEnvironment environment)
    {
        _dataPath = Path.Combine(environment.ContentRootPath, "Data");
    }

    public T Load<T>(string fileName)
    {
        var filePath = Path.Combine(_dataPath, fileName);
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json, JsonOptions)
            ?? throw new InvalidOperationException($"Failed to deserialize '{fileName}'.");
    }
}
