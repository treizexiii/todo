using System.Reflection.Metadata;
using Core.Entities;
using Core.Repositories;
using Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class TodosRepository(TodoDb context) : ITodosRepository
{
    public async Task<IEnumerable<Todo>> GetAllAsync(TodoFilterOptions? filterOptions)
    {
        var query = context.Todos.AsQueryable();
        if (filterOptions is not null)
        {
            if (filterOptions.IsCompleted is not null)
            {
                query = query.Where(t => t.IsCompleted == filterOptions.IsCompleted);
            }

            if (filterOptions.CreatedAt is not null)
            {
                query = query.Where(t => t.CreatedAt >= filterOptions.CreatedAt);
            }

            if (filterOptions.DueDate is not null)
            {
                query = query.Where(t => t.DueDate >= filterOptions.DueDate);
            }
        }

        return await query.OrderBy(t => t.UpdatedAt).ToListAsync();
    }

    public async Task<Todo?> GetAsync(Guid id)
    {
        return await context.Todos.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateAsync(Todo todo)
    {
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todo todo)
    {
        context.Todos.Update(todo);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var todo = await context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        if (todo is not null)
        {
            context.Remove(todo);
            await context.SaveChangesAsync();
        }
    }
}