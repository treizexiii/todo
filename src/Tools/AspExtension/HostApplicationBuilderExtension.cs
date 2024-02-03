using Microsoft.Extensions.Configuration;

namespace AspExtension;

public static class HostApplicationBuilderExtension
{
    public static void ConfigureHost(this IConfigurationBuilder builder, string configPath,
        string? environment, bool optional = false)
    {
        // builder.Sources.Clear();

        //TODO: evolve configure prefix
        builder.AddEnvironmentVariables();

        if (File.Exists($"{configPath}/appsettings.json"))
        {
            builder.AddJsonFile($"{configPath}/appsettings.json", optional);
        }
        if (environment != null && File.Exists($"{configPath}/appsettings.{environment}.json"))
        {
            builder.AddJsonFile($"{configPath}/appsettings.{environment}.json", optional: true);
        }
    }
}
