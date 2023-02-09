using Serilog;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using RuneSharper.Data;
using RuneSharper.Shared.Extensions;
using RuneSharper.Shared.Settings;
using Microsoft.Extensions.DependencyInjection;

await Start();

async Task Start()
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureSerilog(Assembly.GetExecutingAssembly());

    ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();

    await MigrateDatabase(app.Services, app.Environment);

    ConfigureMiddleware(app, app.Environment);
    ConfigureEndpoints(app);

    app.Run();
}


void ConfigureServices(IServiceCollection services, IConfiguration config) {
    services.AddControllers().AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services
        .AddRuneSharperServices()
        .AddIdentityServices(config)
        .AddRuneSharperDatabase(config);

    services.Configure<RuneSharperSettings>(config.GetRequiredSection("RuneSharperSettings"));
}

async Task MigrateDatabase(IServiceProvider services, IWebHostEnvironment env) {
    using var scope = services.CreateScope();

    var servicesCollection = scope.ServiceProvider;

    try {
        var context = servicesCollection.GetRequiredService<RuneSharperContext>();

        await context.Database.MigrateAsync();

        if (env.IsDevelopment()) {
            //await Seed.SeedDataAsync(context);
        }
    } catch (Exception ex) {
        var logger = servicesCollection.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occured during migration");
    }
}

void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env) {
    // Configure the HTTP request pipeline.
    if (env.IsDevelopment()) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

    app.UseHttpsRedirection();

    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app) {
    app.MapControllers();
}