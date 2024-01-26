namespace Persistence.MigrationTool;

public enum CommandAction
{
    Help,
    CreateDatabase,
    UpdateDatabase,
    SeedDatabase
}

public static class CommandActionExtensions
{
    public static CommandAction GetCommandAction(this string args)
    {
        return args switch
        {
            "help" => CommandAction.Help,
            "create-database" => CommandAction.CreateDatabase,
            "update-database" => CommandAction.UpdateDatabase,
            "seed-database" => CommandAction.SeedDatabase,
            _ => throw new ArgumentException("Invalid command")
        };
    }

    public static string GetDescription(this CommandAction action)
    {
        return action switch
        {
            CommandAction.Help => "Prints this help message",
            CommandAction.CreateDatabase => "Creates the database",
            CommandAction.UpdateDatabase => "Updates the database",
            CommandAction.SeedDatabase => "Seeds the database",
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
        };
    }
}
