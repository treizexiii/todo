using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tools.TransactionManager;

public static class TransactionManagerProvider
{
    public static IServiceCollection AddMultiContextTransactionManager(this IServiceCollection services, IEnumerable<Type> contextTypes)
    {

        services.AddScoped<ITransactionManager, MultiContextTransactionManager>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<MultiContextTransactionManager>>();
            var contexts = new List<IDbContext>();
            foreach (var type in contextTypes)
            {
                var context = provider.GetRequiredService(type);
                if (context is not IDbContext dbContext)
                {
                    throw new ArgumentException($"Context {type.Name} is not IDbContext");
                }

                contexts.Add(dbContext);
            }

            return new MultiContextTransactionManager(logger, contexts);

        });
        return services;
    }


    public static IServiceCollection AddTransactionManager<T>(this IServiceCollection services) where T : notnull
    {
        services.AddScoped<ITransactionManager, TransactionManager>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<TransactionManager>>();
            var context = provider.GetRequiredService<T>();
            if (context is not IDbContext dbContext)
            {
                throw new ArgumentException($"Context {typeof(T).Name} is not IDbContext");
            }
            return new TransactionManager(logger, dbContext);
        });

        return services;
    }
}
