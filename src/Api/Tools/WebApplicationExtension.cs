using System.Reflection;
using static System.Console;

namespace Api.Tools;

public static class WebApplicationExtension
{
    public static void StarterLog(this IApplicationBuilder app)
    {

        // var logger = app.ApplicationServices.GetRequiredService<ILogger<WebApplication>>();
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        WriteLine("Application started");
        WriteLine("Environment: {0}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        WriteLine("Application name: {0}", Assembly.GetEntryAssembly()?.GetName().Name);
        WriteLine("Application version: {0}", Assembly.GetEntryAssembly()?.GetName().Version);
        WriteLine("Application started at: {0}", DateTime.Now);
        WriteLine("Database name: {0}", configuration["DbSecret:Database"]);
        WriteLine("Database server: {0}", configuration["DbSecret:Host"]);
    }
}