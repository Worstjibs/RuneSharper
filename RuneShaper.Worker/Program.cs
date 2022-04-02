using Microsoft.EntityFrameworkCore;
using RuneShaper.Worker;
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
    IHost host = Host.CreateDefaultBuilder(args)
        .UseSerilog((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration))
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

            services.AddHostedService<Worker>();
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