using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Database.Context;

namespace Persistence.Database.Repositories;

public class ItemsRepository(TodoDb context) : IItemsRepository
{
    public async Task<IEnumerable<Item>> GetAllAsync(Guid todoId)
    {
        return await context.Items.Where(i => i.TodoId == todoId).ToListAsync();
    }

    public async Task<Item?> GetAsync(Guid itemId)
    {
        return await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
    }

    public async Task CreateAsync(Item item)
    {
        await context.Items.AddAsync(item);
    }

    public Task UpdateAsync(Item item)
    {
        context.Items.Update(item);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid itemId)
    {
        var item = await context.Items.FirstOrDefaultAsync(i => i.Id == itemId);
        if (item is not null)
        {
            context.Remove(item);
        }
    }

    public async Task<bool> ControlListAsync(Guid todoId)
    {
        return await context.Todos.AnyAsync(t => t.Id == todoId);
    }
}