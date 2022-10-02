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

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting RuneSharper.Worker");

    IHost host = Host.CreateDefaultBuilder(args)
        .UseSerilog((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration)
            .WriteTo.Console())
        .ConfigureServices((hostContext, services) =>
        {
            var config = hostContext.Configuration;

            services.Configure<RuneSharperSettings>(config.GetSection("RuneSharperSettings"));
            services.AddSingleton<IOsrsApiService, OsrsApiService>();
            services.AddScoped<ISaveStatsService, SaveStatsService>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            services.AddDbContext<RuneSharperContext>(options => {
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

    await host.RunAsync();
} catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}