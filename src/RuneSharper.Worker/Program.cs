using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using DotnetOsrsApiWrapper;
using Serilog;
using RuneSharper.Worker;
using RuneSharper.Data;
using RuneSharper.Data.Repositories;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Services.SaveStats;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Settings;
using RuneSharper.Shared.Extensions;

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    log.Information("Starting RuneSharper.Worker");

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureSerilog(Assembly.GetExecutingAssembly())
        .ConfigureServices((hostContext, services) =>
        {
            var config = hostContext.Configuration;

            services.AddMemoryCache();

            services.Configure<RuneSharperSettings>(config.GetSection("RuneSharperSettings"));
            services.AddSingleton<IOsrsApiService, OsrsApiService>();
            services.AddScoped<ISaveStatsService, SaveStatsService>();

            services.AddScoped<ICharacterRepository, CachedCharacterRepository>();
            services.AddScoped<CharacterRepository>();

            services.AddScoped<ISnapshotRepository, CachedSnapshotRepostiory>();
            services.AddScoped<SnapshotRepository>();

            services.AddDbContext<RuneSharperContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddAzureClients(builder =>
            {
                var connectionString = hostContext.HostingEnvironment.EnvironmentName switch
                {
                    "Development" => "ServiceBusDev",
                    "Production" => "ServiceBusProd",
                    _ => throw new Exception("EnvironmentName is invalid")
                };

                builder.AddServiceBusClient(config.GetConnectionString(connectionString));
            });

            services.AddHostedService<StatsWorker>();
            //services.AddHostedService<RuneSharperMessageWorker>();

            services.AddOsrsWrapper();
        })
        .UseWindowsService(options =>
        {
            options.ServiceName = "RuneSharper Worker Service";
        })
        .Build();

    using (var scope = host.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<RuneSharperContext>();
        await context.Database.MigrateAsync();
    }

    await host.RunAsync();
} catch (Exception ex)
{
    log.Fatal(ex, "Unhandled Exception");
} finally
{
    log.Information("Shut down complete");
}