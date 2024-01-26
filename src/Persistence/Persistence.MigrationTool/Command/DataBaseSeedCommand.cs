using Microsoft.Extensions.Logging;
using Persistence.Database.Context;

namespace Persistence.MigrationTool.Command;

public class DataBaseSeedCommand : CommandBase
{
    public DataBaseSeedCommand(ILogger<DataBaseSeedCommand> logger, TodoDb context) : base(logger, context)
    {
    }

    public override async Task ExecuteAsync()
    {
        Logger.LogInformation("Seeding database");
        await Context.SeedAsync();
        Logger.LogInformation("Database seeded");
    }
}