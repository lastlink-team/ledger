using System.Net.Http.Headers;
using Lastlink.Ledger.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Lastlink.Ledger.Infrastructure.Http;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly LedgerSettings _settings;

    public AuthHeaderHandler(IOptions<LedgerSettings> settings)
    {
        _settings = settings.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_settings.ApiKey))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiKey);

        return base.SendAsync(request, cancellationToken);
    }
}
