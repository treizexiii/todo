using Core.Entities;
using Core.Factories;

namespace Persistence.Database.Factories;

public class TodoDso
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public virtual List<ItemDso> Items { get; set; } = new();
}

public class ItemDso
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public Guid TodoId { get; set; }
    public virtual TodoDso Todo { get; set; } = null!;
}

public class TodoConverter : ITodoConverter<TodoDso>, IItemFactory<ItemDso>
{
    public TodoDso ConvertTodo(Todo todo)
    {
        return new TodoDso
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            DueDate = todo.DueDate,
            CompletedAt = todo.CompletedAt,
            Items =  todo.Items.Select(ConvertItem).ToList(),
        };
    }

    public Todo ConvertTodo(TodoDso todo)
    {
        return new Todo
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            DueDate = todo.DueDate,
            CompletedAt = todo.CompletedAt,
            Items = todo.Items.Select(ConvertItem).ToList(),
        };
    }

    public ItemDso ConvertItem(Item item)
    {
        return new ItemDso
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsCompleted = item.IsCompleted,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            DueDate = item.DueDate,
            CompletedAt = item.CompletedAt,
            TodoId = item.TodoId,
        };
    }

    public Item ConvertItem(ItemDso item)
    {
        return new Item
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsCompleted = item.IsCompleted,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            DueDate = item.DueDate,
            CompletedAt = item.CompletedAt,
            TodoId = item.TodoId,
        };
    }
}