using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Authentication.Persistence.RepositoriesImp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Authentication.Persistence;

public static class DatabaseProvider
{
    public static IServiceCollection AddIdentityDbInMemory(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityDb>(options =>
        {
            options.UseInMemoryDatabase(connectionString);
        });
        services.AddRepositories();

        return services;
    }

    public static IServiceCollection AddIdentityDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityDb>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseLowerCaseNamingConvention();
            options.UseLazyLoadingProxies();
        });
        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IClaimsRepository, ClaimsRepository>();
        services.AddTransient<ISecretsRepository, SecretsRepository>();
    }

    public static string BuildPostgresConnectionString(this IConfiguration configuration, string contextName)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = configuration[$"{contextName}DbSecret:Host"],
            Port = int.Parse(configuration[$"{contextName}DbSecret:Port"]),
            Username = configuration[$"{contextName}DbSecret:User"],
            Password = configuration[$"{contextName}DbSecret:Password"],
            Database = configuration[$"{contextName}DbSecret:Database"]
        };

        return builder.ConnectionString;
    }
}