using Core.Repositories;
using Database.Context;
using Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class ContextProvider
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