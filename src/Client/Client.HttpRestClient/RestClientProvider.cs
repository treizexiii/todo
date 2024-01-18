using Client.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Client.HttpRestClient;

public static class RestClientProvider
{
    public static IServiceCollection AddRestClient(this IServiceCollection services, string url)
    {
        services.AddHttpClient<ITodoServiceProxy, TodoHttpRestClient>(client =>
        {
            client.BaseAddress = new Uri(url);
        });
        services.AddHttpClient<IItemServiceProxy, TodoHttpRestClient>(client =>
        {
            client.BaseAddress = new Uri(url);
        });

        return services;
    }
}