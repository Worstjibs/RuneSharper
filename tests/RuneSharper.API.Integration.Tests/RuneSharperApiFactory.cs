using System.Data.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Respawn;
using Serilog;
using RuneSharper.Data;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Data.Repositories;

namespace RuneSharper.API.Integration.Tests;

public class RuneSharperApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private const string _imageName = "mcr.microsoft.com/mssql/server:2019-latest";

    private readonly TestcontainerDatabase _dbContainer =
        new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithName("sql1")
            .WithDatabase(new MsSqlTestcontainerConfiguration(_imageName)
            {
                Password = "TuxUNPjhIJmttJ1pMhF",
                Database = "runesharper"
            })
            //.WithPortBinding(1433, 1433)
            .Build();

    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    private string ConnectionString => $"{_dbContainer.ConnectionString}TrustServerCertificate=True;";

    public HttpClient HttpClient { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x => x.ClearProviders());

#pragma warning disable CS0618
        builder.UseSerilog((_, _) => { });
#pragma warning restore CS0618

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<RuneSharperContext>();
            services.RemoveAll<DbContextOptions<RuneSharperContext>>();

            services.RemoveAll<ICharacterRepository>();
            services.RemoveAll<CharacterRepository>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            services.AddDbContext<RuneSharperContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        HttpClient = CreateClient();

        _dbConnection = new SqlConnection(ConnectionString);
        await _dbConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new[] { "dbo" }
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    public async Task ResetDb()
    {
        await _respawner.ResetAsync(_dbConnection);
    }
}
