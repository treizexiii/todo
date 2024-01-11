using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreProvider
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}