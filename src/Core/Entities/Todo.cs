namespace Core.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    // public TodoType TodoType { get; set; } = new();
    public bool IsCompleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public virtual List<Item> Items { get; set; } = new();
}

public class TodoType
{
    public Guid Id { get; set; }
    public string Type { get; set; } = TodoTypeEnumeration.None;
}

public class TodoTypeEnumeration
{
    public static string None => "None";
    public static string Work => "Work";
    public static string Shopping => "Shopping";
}