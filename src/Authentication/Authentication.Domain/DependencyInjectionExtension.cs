using Authentication.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Domain;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddAuthenticationDomain(this IServiceCollection services)
    {
        services.AddScoped<ISecretService, SecretService>();

        return services;
    }
}