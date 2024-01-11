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

        services.AddScoped<ITodosRepository, TodosRepository>();
        services.AddScoped<IItemsRepository, ItemsRepository>();

        return services;
    }
}