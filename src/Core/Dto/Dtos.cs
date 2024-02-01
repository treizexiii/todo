using Core.Entities;

namespace Core.Dto;

public record TodoHeaderDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt);

public record TodoDto(
    Guid Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt,
    List<ItemDto> Items);

public record ItemDto(
    Guid Id,
    Guid TodoId,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? DueDate,
    DateTime? CompletedAt);

public record CreateTodo(string Title, string? Description, DateTime? DueDate);

public record UpdateTodo(Guid Id, string Title, string? Description, DateTime? DueDate);

public record CreateItem(string Title, string? Description, DateTime? DueDate, Guid TodoId);

public record UpdateItem(Guid Id, string Title, string? Description, DateTime? DueDate, Guid TodoId);

public static class DtoExtension
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt.DateTime,
            todo.UpdatedAt.DateTime,
            todo.DueDate?.DateTime,
            todo.CompletedAt?.DateTime,
            todo.Items.Select(item => item.ToDto()).ToList());
    }

    public static TodoHeaderDto ToHeaderDto(this Todo todo)
    {
        return new TodoHeaderDto(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsCompleted,
            todo.CreatedAt.DateTime,
            todo.UpdatedAt.DateTime,
            todo.DueDate?.DateTime,
            todo.CompletedAt?.DateTime);
    }

    public static ItemDto ToDto(this Item item)
    {
        return new ItemDto(
            item.Id,
            item.TodoId,
            item.Title,
            item.Description,
            item.IsCompleted,
            item.CreatedAt.DateTime,
            item.UpdatedAt.DateTime,
            item.DueDate?.DateTime,
            item.CompletedAt?.DateTime);
    }
}