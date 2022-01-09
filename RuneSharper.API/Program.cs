using Microsoft.EntityFrameworkCore;
using RuneSharper.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

await MigrateDatabase(app.Services, app.Environment);

ConfigureMiddleware(app, app.Environment);
ConfigureEndpoints(app);

app.Run();


void ConfigureServices(IServiceCollection services) {
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddRuneSharperServices()
        .AddRuneSharperDatabase(builder.Configuration);
}

async Task MigrateDatabase(IServiceProvider services, IWebHostEnvironment env) {
    using var scope = services.CreateScope();

    var servicesCollection = scope.ServiceProvider;

    try {
        var context = servicesCollection.GetRequiredService<RuneSharperContext>();

        await context.Database.MigrateAsync();

        if (env.IsDevelopment()) {
        }
    } catch (Exception ex) {
        var logger = servicesCollection.GetService<ILogger<Program>>();

        logger.LogError(ex, "An error occured during migration");
    }
}

void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env) {
    // Configure the HTTP request pipeline.
    if (env.IsDevelopment()) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app) {
    app.MapControllers();
}