using Microsoft.Extensions.Configuration;

namespace AspExtension;

public static class HostApplicationBuilderExtension
{
    public static void ConfigureHost(this IConfigurationBuilder builder, string configPath,
        string? environment)
    {
        builder.Sources.Clear();

        //TODO: evolve configure prefix
        builder.AddEnvironmentVariables();

        builder.AddJsonFile($"{configPath}/appsettings.json", optional: false);
        if (environment != null)
        {
            builder.AddJsonFile($"{configPath}/appsettings.{environment}.json", optional: true);
        }
    }
}