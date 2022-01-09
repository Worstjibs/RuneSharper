using Microsoft.EntityFrameworkCore;
using RuneSharper.Data;
using RuneSharper.Services.HostedServices;
using RuneSharper.Services.Stats;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensionMethods {
    public static IServiceCollection AddRuneSharperServices(this IServiceCollection services) {
        services.AddSingleton<IStatsService, StatsService>();

        services.AddHostedService<StatsTimerService>();

        return services;
    }

    public static IServiceCollection AddRuneSharperDatabase(this IServiceCollection services, IConfiguration config) {
        services.AddDbContext<RuneSharperContext>(options => {
            options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
