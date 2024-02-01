using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ITodosRepository, TodosRepository>();
        services.AddScoped<IItemsRepository, ItemsRepository>();
    }
}