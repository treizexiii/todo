using Core.Entities;

namespace Core.Factories;

public interface ITodoConverter<T> where T : class
{
    T ConvertTodo(Todo todo);
    Todo ConvertTodo(T todo);
}

public interface IItemFactory<T> where T : class
{
    T ConvertItem(Item item);
    Item ConvertItem(T item);
}