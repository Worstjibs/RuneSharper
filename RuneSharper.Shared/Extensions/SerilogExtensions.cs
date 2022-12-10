﻿using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace RuneSharper.Shared.Extensions;
public static class SerilogExtensions
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, Assembly executingAssemly)
    {
        builder.UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(ctx.Configuration["ElasticConfiguration:Uri"]))
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
                .Enrich.WithProperty("ServiceName", executingAssemly.GetName().Name!.ToLower().Replace(".", "-"));
        });

        return builder;
    }
}