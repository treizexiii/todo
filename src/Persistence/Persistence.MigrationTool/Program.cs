// See https://aka.ms/new-console-template for more information

using AspExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Database;
using Persistence.MigrationTool.Command;

namespace Persistence.MigrationTool;

internal static class Program
{
    private const string CONFIG_PATH = "Configuration";
    private static readonly string Environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Todo-db!");

        var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var host = BuildHost(env);
        // await host.RunAsync();

        var factory = new CommandFactory(host.Services);
        await factory.ExecuteAsync(args);
    }

    private static IHost BuildHost(string? env)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(b =>
            {
                b.ConfigureHost(CONFIG_PATH, env);
            });

        builder.ConfigureServices((context, services) =>
        {
            services.AddLogging();
            services.AddPostgresContext(context.Configuration.GetConnectionString("TodoDb") ??
                                        throw new InvalidOperationException("TodoDb connection string is null"));
            services.AddScoped<HelpCommand>();
            services.AddScoped<DatabaseUpdateCommand>();
            services.AddScoped<DataBaseSeedCommand>();
            services.AddScoped<DatabaseCreateCommand>();
        });

        var host = builder.Build();

        return host;
    }
}