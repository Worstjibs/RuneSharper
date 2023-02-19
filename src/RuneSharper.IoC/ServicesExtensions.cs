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
using RuneSharper.Application.Services.Characters;
using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Application.Services.LineCharts;
using RuneSharper.Application.Services.SaveStats;
using RuneSharper.Application.Services.Snapshots;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation;
using RuneSharper.Application.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Application.Services.Stats;
using RuneSharper.Application.Services.Token;
using RuneSharper.Domain.Entities.Users;
using RuneSharper.Application.Models;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Application.Settings;
using System.Reflection;

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
        services.Scan(i =>
            i.FromAssemblies(Assembly.GetAssembly(typeof(IChangeAggregationStrategy))!)
            .AddClasses(c => c.AssignableTo<IChangeAggregationStrategy>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

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
