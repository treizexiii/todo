using Microsoft.Extensions.DependencyInjection;
using TodoClient.Implementations;
using TodoClient.Interfaces;

namespace TodoClient;

public static class ClientInterfaceProvider
{
    public static IServiceCollection AddRestClient(this IServiceCollection services, string url)
    {
        services.AddHttpClient<ITodoServiceProxy, TodoServiceHttpClient>(client =>
        {
            client.BaseAddress = new Uri(url);
        });

        return services;
    }

    public static IServiceCollection AddGrpcClient(this IServiceCollection services, string url)
    {
        services.AddScoped<ITodoServiceProxy>(_ => new TodoServiceGrpcClient(url));

        return services;
    }
}