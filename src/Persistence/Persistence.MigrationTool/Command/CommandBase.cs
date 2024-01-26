using Microsoft.Extensions.Logging;
using Persistence.Database.Context;

namespace Persistence.MigrationTool.Command;

public interface ICommand
{
    Task ExecuteAsync();
}

public abstract class CommandBase : ICommand
{
    protected readonly ILogger<CommandBase> Logger;
    protected readonly TodoDb Context;

    protected CommandBase(ILogger<CommandBase> logger, TodoDb context)
    {
        Logger = logger;
        Context = context;
    }

    public abstract Task ExecuteAsync();
}