namespace Core.Entities;

public class SuggestedItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    // public TodoType TodoType { get; set; } = new();
    public int Count { get; set; }
    public int Priority { get; set; }
    public int Frequency { get; set; }
}