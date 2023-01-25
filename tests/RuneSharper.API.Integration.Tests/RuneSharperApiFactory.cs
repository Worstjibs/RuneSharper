using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RuneSharper.API.Integration.Tests;

public class RuneSharperApiFactory : WebApplicationFactory<RuneSharper.API.Controllers.BaseApiController>, IAsyncLifetime
{
    public HttpClient HttpClient { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x => x.ClearProviders());

        builder.ConfigureTestServices(services =>
        {
        });
    }

    public Task InitializeAsync()
    {
        HttpClient = CreateClient();
        return Task.CompletedTask;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
