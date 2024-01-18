using Authentication.Services.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Services;

public static class IdentityServicesProvider
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminUser, AdminService>();
        services.AddScoped<IAdminClaim, AdminService>();

        return services;
    }
}