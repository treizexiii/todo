
using Microsoft.Extensions.Logging;
using Persistence.Database.Context;
using static System.Console;

namespace Persistence.MigrationTool.Command;

public class HelpCommand : CommandBase
{
    public HelpCommand(ILogger<HelpCommand> logger, TodoDb context) : base(logger, context)
    {
    }

    public override Task ExecuteAsync()
    {
        WriteLine("Available commands:");
        WriteLine("  create-database: " + CommandAction.CreateDatabase.GetDescription());
        WriteLine("  update-database: " + CommandAction.UpdateDatabase.GetDescription());
        WriteLine("  seed-database: " + CommandAction.SeedDatabase.GetDescription());

        return Task.CompletedTask;
    }
}