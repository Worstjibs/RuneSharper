using System.Reflection;
using Microsoft.Extensions.Azure;
using Microsoft.EntityFrameworkCore;
using DotnetOsrsApiWrapper;
using Serilog;
using RuneSharper.Data;
using RuneSharper.IoC;
using RuneSharper.Application.Settings;
using RuneSharper.Worker;

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

            services.Configure<RuneSharperSettings>(config.GetRequiredSection("RuneSharperSettings"));

            services
                .AddRuneSharperServices()
                .AddRuneSharperDatabase(config);

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