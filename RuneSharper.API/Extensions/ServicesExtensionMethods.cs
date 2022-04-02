using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RuneSharper.Data;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.Stats;
using RuneSharper.Services.Token;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Settings;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServicesExtensionMethods {
    public static IServiceCollection AddRuneSharperServices(this IServiceCollection services, IConfiguration config) {
        services.Configure<RuneSharperSettings>(config.GetSection("RuneSharperSettings"));
        services.AddSingleton<IOsrsApiService, OsrsApiService>();
        services.AddScoped<CharacterRepository, CharacterRepository>();

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


        var jwtSection = config.GetSection("Jwt");
        services.Configure<JwtTokenSettings>(jwtSection);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Get<JwtTokenSettings>().SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    public static IServiceCollection AddRuneSharperDatabase(this IServiceCollection services, IConfiguration config) {
        services.AddDbContext<RuneSharperContext>(options => {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
