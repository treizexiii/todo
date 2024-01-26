using Microsoft.Extensions.DependencyInjection;
using TodoClient.Implementations;
using TodoClient.Interfaces;

namespace TodoClient;

public static class ClientInterfaceProvider
{
    public static IServiceCollection AddGrpcClient(this IServiceCollection services, string url)
    {
        services.AddScoped<ITodoServiceProxy>(_ => new TodoServiceGrpcClient(url));

        return services;
    }
}