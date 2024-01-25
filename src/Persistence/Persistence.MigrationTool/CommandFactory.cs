using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.MigrationTool.Command;

namespace Persistence.MigrationTool;

public class CommandFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CommandFactory> _logger;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = serviceProvider.GetRequiredService<ILogger<CommandFactory>>();
    }

    public async Task ExecuteAsync(string[] args)
    {
        try
        {
            var action = args[0].GetCommandAction();
            ICommand command = action switch
            {
                CommandAction.Help => _serviceProvider.GetRequiredService<HelpCommand>(),
                CommandAction.CreateDatabase => _serviceProvider.GetRequiredService<DatabaseCreateCommand>(),
                CommandAction.UpdateDatabase => _serviceProvider.GetRequiredService<DatabaseUpdateCommand>(),
                CommandAction.SeedDatabase => _serviceProvider.GetRequiredService<DataBaseSeedCommand>(),
                _ => throw new ArgumentOutOfRangeException()
            };

            await command.ExecuteAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error executing command");
        }
    }
}
