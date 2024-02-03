using Api.Tools;
using AspExtension;

namespace Api;

public class Program
{
    private const string CONFIG_PATH = "Configuration";

    private static readonly string AspEnvironment =
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.ConfigureHost(CONFIG_PATH, AspEnvironment);
        builder.AddServices();
        var app = builder.Build();
        app.StarterLog();
        app.ConfigureApp();
        app.Run();
    }


}
