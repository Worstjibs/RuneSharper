using DotnetOsrsApiWrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using RuneSharper.Worker;
using RuneSharper.Data;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.SaveStats;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Settings;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting RuneSharper.Worker");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(ctx.Configuration["ElasticConfiguration:Uri"]))
                {
                    IndexFormat = $"runesharper" +
                         $"-logs" +
                         $"-{ctx.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}" +
                         $"-{DateTime.UtcNow:yyyy-MM}",
                    AutoRegisterTemplate = false,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                })
                .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("ServiceName", Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-"));
        })
        .ConfigureServices((hostContext, services) =>
        {
            var config = hostContext.Configuration;

            services.Configure<RuneSharperSettings>(config.GetSection("RuneSharperSettings"));
            services.AddSingleton<IOsrsApiService, OsrsApiService>();
            services.AddScoped<ISaveStatsService, SaveStatsService>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();

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
    Log.Fatal(ex, "Unhandled Exception");
} finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}