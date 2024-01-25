using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.MigrationTool.Command;

public class HelpCommand : ICommand
{
    private readonly ILogger<HelpCommand> _logger;

    public HelpCommand(ILogger<HelpCommand> logger)
    {
        _logger = logger;
    }

    public Task ExecuteAsync()
    {
        _logger.LogInformation("Available commands:");
        _logger.LogInformation("  create-database");
        _logger.LogInformation("  update-database");
        _logger.LogInformation("  seed-database");

        return Task.CompletedTask;
    }
}

public class DataBaseSeedCommand : ICommand
{
    private readonly ILogger<DataBaseSeedCommand> _logger;
    private readonly TodoDb _context;

    public DataBaseSeedCommand(ILogger<DataBaseSeedCommand> logger, TodoDb context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("Seeding database");
        // await _context.SeedAsync();

        _logger.LogInformation("Database seeded");
    }
}

public class DatabaseCreateCommand : ICommand
{
    private readonly ILogger<DatabaseCreateCommand> _logger;
    private readonly TodoDb _context;

    public DatabaseCreateCommand(ILogger<DatabaseCreateCommand> logger, TodoDb context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("Creating database");
        await _context.Database.EnsureCreatedAsync();
        _logger.LogInformation("Database created");
    }
}

public class DatabaseUpdateCommand : ICommand
{
    private readonly ILogger<DatabaseUpdateCommand> _logger;
    private readonly TodoDb _context;

    private DatabaseUpdateCommand(ILogger<DatabaseUpdateCommand> logger, TodoDb context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            _logger.LogInformation("Migrating database");
            await _context.Database.MigrateAsync();
            // await _context.SaveChangesAsync();
            _logger.LogInformation("Database updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error migrating database");
        }

    }
}