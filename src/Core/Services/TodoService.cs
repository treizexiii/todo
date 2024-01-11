using Core.Dto;
using Core.Entities;
using Core.Repositories;

namespace Core.Services;

public class TodoService(ITodosRepository repository) : ITodoService
{
    public async Task<IEnumerable<Todo>> GetAllAsync(TodoFilterOptions? filterOptions)
    {
        return await repository.GetAllAsync(filterOptions);
    }

    public async Task<Todo?> GetAsync(Guid id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<Todo> CreateAsync(CreateTodo todo)
    {
        var utc = DateTime.UtcNow;
        var todoEntity = new Todo
        {
            Id = Guid.NewGuid(),
            Title = todo.Title,
            Description = todo.Description ?? string.Empty,
            DueDate = todo.DueDate,
            IsCompleted = false,
            CreatedAt = utc,
            UpdatedAt = utc
        };

        await repository.CreateAsync(todoEntity);
        return todoEntity;
    }

    public async Task<Todo> UpdateAsync(Guid id, UpdateTodo todo)
    {
        var todoEntity = await repository.GetAsync(id);
        if (todoEntity is null) throw new FileNotFoundException($"Todo with id {id} not found");

        todoEntity.Title = todo.Title;
        todoEntity.Description = todo.Description ?? string.Empty;
        todoEntity.DueDate = todo.DueDate;
        todoEntity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(todoEntity);
        return todoEntity;
    }

    public async Task<Todo> CompleteAsync(Guid id)
    {
        var todoEntity = await repository.GetAsync(id);
        if (todoEntity is null) throw new FileNotFoundException($"Todo with id {id} not found");

        var utc = DateTime.UtcNow;
        todoEntity.IsCompleted = true;
        todoEntity.CompletedAt = utc;
        todoEntity.UpdatedAt = utc;

        await repository.UpdateAsync(todoEntity);
        return todoEntity;
    }
}