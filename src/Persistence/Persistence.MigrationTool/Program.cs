// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Database;
using Persistence.MigrationTool.Command;

namespace Persistence.MigrationTool;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Todo-db!");

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var host = BuildHost(env);
        // await host.RunAsync();

        var factory = new CommandFactory(host.Services);
        await factory.ExecuteAsync(args);
    }

    private static IHost BuildHost(string? env)
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureHostConfiguration(configuration =>
        {
            configuration.AddEnvironmentVariables();
            configuration.AddJsonFile("appsettings.json", optional: false);
            if (env != null)
            {
                configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
            }
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