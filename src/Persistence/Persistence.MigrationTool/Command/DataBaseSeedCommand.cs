using DataSets;
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

        var todoTypes = TodoTypes.GetTodoTypeList();
        await Context.TodoTypes.AddRangeAsync(todoTypes);

        // var suggestedItems = SuggestedItems.GetSuggestedItemList();
        // await Context.SuggestedItems.AddRangeAsync(suggestedItems);

        await Context.SaveChangesAsync();

        Logger.LogInformation("Database seeded");
    }
}