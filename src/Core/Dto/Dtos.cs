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
    IEnumerable<ItemDto> Items);

public record ItemDto(
    Guid Id,
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