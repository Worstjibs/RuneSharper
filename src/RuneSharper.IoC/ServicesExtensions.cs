using System.Text;
using DotnetOsrsApiWrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RuneSharper.Data;
using RuneSharper.Data.Repositories;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Services.Services.Characters;
using RuneSharper.Services.Services.DateTimeProvider;
using RuneSharper.Services.Services.LineCharts;
using RuneSharper.Services.Services.SaveStats;
using RuneSharper.Services.Services.Snapshots;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation;
using RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Services.Services.Stats;
using RuneSharper.Services.Services.Token;
using RuneSharper.Domain.Entities.Users;
using RuneSharper.Services.Models;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Services.Settings;

namespace RuneSharper.IoC;

public static class ServicesExtensions
{
    public static IServiceCollection AddRuneSharperServices(this IServiceCollection services)
    {
        services.AddSingleton<IOsrsApiService, OsrsApiService>();
        services.AddOsrsWrapper();

        services.AddMemoryCache();

        services.AddScoped<ICharacterRepository, CachedCharacterRepository>();
        services.AddScoped<CharacterRepository>();

        services.AddScoped<ISnapshotRepository, CachedSnapshotRepostiory>();
        services.AddScoped<SnapshotRepository>();

        services.AddScoped<ISkillSnapshotRepository, SkillSnapshotRepository>();

        services.AddScoped<ISaveStatsService, SaveStatsService>();

        services.AddScoped<ICharactersService, CharactersService>();
        services.AddScoped<ISnapshotsService, SnapshotsService>();

        services.AddScoped<ILineChartService, LineChartService>();

        services.AddScoped<IChangeAggregationHandler<ActivitiesChangeModel>, ActivitiesChangeAggregationHandler>();
        services.AddScoped<IChangeAggregationHandler<StatsChangeModel>, StatsChangeAggregationHandler>();
        services.AddScoped<IChangeAggregationStrategy, DayChangeStrategy>();
        services.AddScoped<IChangeAggregationStrategy, WeekChangeStrategy>();
        services.AddScoped<IChangeAggregationStrategy, MonthChangeStrategy>();
        services.AddScoped<IChangeAggregationStrategy, YearChangeStrategy>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddIdentityCore<AppUser>(options =>
        {
            // Password Settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 10;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
            options.User.RequireUniqueEmail = true;
        })
            .AddUserManager<UserManager<AppUser>>()
            .AddEntityFrameworkStores<RuneSharperContext>();


        var jwtSection = config.GetRequiredSection("Jwt");
        services.Configure<JwtTokenSettings>(jwtSection);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Get<JwtTokenSettings>()!.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    public static IServiceCollection AddRuneSharperDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<RuneSharperContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")!);
        });

        return services;
    }
}
