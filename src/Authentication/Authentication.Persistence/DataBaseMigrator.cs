using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence;

public class DataBaseMigrator(DbContext context)
{
    public async Task CheckDatabase()
    {
        var created = await Create();
        if (created == 1) return;

        if (await AnyMigrationPending())
        {
            await Migrate();
        }
    }

    private async Task<int> Create()
    {
        if (await context.Database.CanConnectAsync()) return 0;

        await context.Database.EnsureCreatedAsync();
        return 1;
    }

    private async Task Migrate()
    {
        await context.Database.MigrateAsync();
    }

    private async Task<bool> AnyMigrationPending()
    {
        return (await context.Database.GetPendingMigrationsAsync()).Any();
    }
}