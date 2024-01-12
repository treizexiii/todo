
using Microsoft.Extensions.DependencyInjection;

namespace GrpcClient;

public static class GrpcClientProvider
{
    public static IServiceCollection AddGrpcClient(this IServiceCollection services, string url)
    {
        services.AddScoped<TodoServiceProxy>(_ => new TodoServiceProxy(url));
        services.AddScoped<ItemServiceProxy>(_ => new ItemServiceProxy(url));

        return services;
    }
}