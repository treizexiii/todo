using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Database.Context;

namespace Persistence.MigrationTool.Command;

public class DatabaseUpdateCommand : CommandBase
{
    public DatabaseUpdateCommand(ILogger<DatabaseUpdateCommand> logger, TodoDb context) : base(logger, context)
    {
    }

    public override async Task ExecuteAsync()
    {
        try
        {
            Logger.LogInformation("Migrating database");
            await Context.Database.MigrateAsync();
            Logger.LogInformation("Database updated");
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Error migrating database");
        }

    }
}