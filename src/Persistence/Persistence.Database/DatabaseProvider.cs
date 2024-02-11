using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence.Database.Context;
using Persistence.Database.Repositories;

namespace Persistence.Database;

public static class DatabaseProvider
{
    public static IServiceCollection AddInMemoryContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoDb>(options =>
        {
            options.UseInMemoryDatabase(connectionString);
        });
        AddRepositories(services);

        return services;
    }

    public static IServiceCollection AddPostgresContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoDb>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseLowerCaseNamingConvention();
            options.UseLazyLoadingProxies();
        });
        AddRepositories(services);

        return services;
    }

    public static string BuildPostgresConnectionString(this IConfiguration configuration)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = configuration["DbSecret:Host"],
            Port = int.Parse(configuration["DbSecret:Port"]),
            Username = configuration["DbSecret:User"],
            Password = configuration["DbSecret:Password"],
            Database = configuration["DbSecret:Database"]
        };

        return builder.ConnectionString;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ITodosRepository, TodosRepository>();
        services.AddScoped<IItemsRepository, ItemsRepository>();
        services.AddScoped<ISuggestedItemsRepository, SuggestedItemsRepository>();
    }
}
