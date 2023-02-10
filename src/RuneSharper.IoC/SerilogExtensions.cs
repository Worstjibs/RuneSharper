using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace RuneSharper.IoC;

public static class SerilogExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, Assembly executingAssemly)
    {
        builder.UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration)
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(ctx.Configuration.GetRequiredSection("ElasticConfiguration:Uri").Value!))
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
                .Enrich.WithProperty("ServiceName", executingAssemly.GetName().Name!.ToLower().Replace(".", "-"))
                .Enrich.FromLogContext()
                .Enrich.WithMachineName();
        });

        return builder;
    }
}
