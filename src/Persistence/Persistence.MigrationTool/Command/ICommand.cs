namespace Persistence.MigrationTool.Command;

public interface ICommand
{
    Task ExecuteAsync();
}