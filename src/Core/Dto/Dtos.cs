namespace Core.Dto;

public record CreateTodo(string Title, string? Description, DateTime? DueDate);

public record UpdateTodo(string Title, string? Description, DateTime? DueDate);

public record CreateItem(string Title, string? Description, DateTime? DueDate);

public record UpdateItem(string Title, string? Description, DateTime? DueDate);