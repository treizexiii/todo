using Core.Entities;

namespace Core.Repositories;

public interface ITodosRepository
{
    Task<IEnumerable<Todo>> GetAllAsync(TodoFilterOptions? filterOptions);
    Task<Todo?> GetAsync(Guid id);
    Task CreateAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(Guid id);
}

public class TodoFilterOptions
{
    public bool? IsCompleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
}