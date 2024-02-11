using Core.Entities;

namespace DataSets;

public class TodoTypes
{
    public static readonly TodoType None = new()
    {
        Id = Guid.Parse("1b697df9-b75b-4cee-b5b7-603fdb9c1668"),
        Type = "None",
    };

    public static readonly TodoType Work = new()
    {
        Id = Guid.Parse("263ae822-db84-4d28-97b9-67bbba5e4a07"),
        Type = "Work",
    };

    public static readonly TodoType Shopping = new()
    {
        Id = Guid.Parse("00575508-785f-4869-9447-1e1c79e913a0"),
        Type = "Shopping",
    };

    public static List<TodoType> GetTodoTypeList()
    {
        return
        [
            None,
            Work,
            Shopping
        ];
    }
}