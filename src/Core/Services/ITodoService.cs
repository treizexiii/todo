using Core.Dto;
using Core.Entities;
using Core.Repositories;

namespace Core.Services;

public interface ITodoService
{
    public Task<IEnumerable<Todo>> GetAllAsync(TodoFilterOptions? filterOptions);
    public Task<Todo?> GetAsync(Guid id);
    public Task<Todo> CreateAsync(CreateTodo todo);
    Task<Todo> UpdateAsync(Guid id, UpdateTodo todo);
    Task<Todo> CompleteAsync(Guid id);
}

