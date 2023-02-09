using System.Net.Http;
using System.Threading.Tasks;

namespace RuneSharper.API.Integration.Tests;

[Collection("Shared collection")]
public class BaseIntegrationTest : IAsyncLifetime
{
    protected readonly RuneSharperApiFactory _waf;
    protected readonly HttpClient _client;

    public BaseIntegrationTest(RuneSharperApiFactory waf)
    {
        _waf = waf;
        _client = _waf.HttpClient;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _waf.ResetPersistance();
}
