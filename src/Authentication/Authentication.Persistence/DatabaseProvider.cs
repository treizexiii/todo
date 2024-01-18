using Authentication.Domain.Repositories;
using Authentication.Persistence.Database;
using Authentication.Persistence.RepositoriesImp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Persistence;

public static class DatabaseProvider
{
    public static IServiceCollection AddIdentityDbInMemory(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityDb>(options =>
        {
            options.UseInMemoryDatabase(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddIdentityDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IdentityDb>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseLowerCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClaimsRepository, ClaimsRepository>();

        return services;
    }
}