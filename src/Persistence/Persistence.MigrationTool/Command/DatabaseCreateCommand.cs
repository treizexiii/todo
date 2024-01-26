using Microsoft.Extensions.Logging;
using Persistence.Database.Context;

namespace Persistence.MigrationTool.Command;

public class DatabaseCreateCommand : CommandBase
{
    public DatabaseCreateCommand(ILogger<DatabaseCreateCommand> logger, TodoDb context) : base(logger, context)
    {
    }

    public override async Task ExecuteAsync()
    {
        Logger.LogInformation("Creating database");
        await Context.Database.EnsureCreatedAsync();
        Logger.LogInformation("Database created");
    }
}