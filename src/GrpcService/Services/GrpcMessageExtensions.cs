using Core.Dto;
using Core.Entities;
using Core.Repositories;
using Google.Protobuf.WellKnownTypes;
using GrpcMessage.Item;
using GrpcMessage.Todo;

namespace GrpcService.Services;

public static class GrpcMessageExtensions
{
    public static TodoFilterOptions TodoFilterOptions(this TodosFilterMessage request)
    {
        var filterOptions = new TodoFilterOptions
        {
            IsCompleted = request.IsCompleted
        };

        return filterOptions;
    }

    public static CreateTodo ToCreateTodoRequest(this CreateTodoMessage request)
    {
        var title = request.Title;
        var description = request.Description;
        DateTime? dueDate = request.DueDate != null ? request.DueDate.ToDateTime() : null;
        return new CreateTodo(title, description, dueDate);
    }

    public static UpdateTodo ToUpdateTodoRequest(this UpdateTodoMessage request)
    {
        var title = request.Title;
        var description = request.Description;
        DateTime? dueDate = request.DueDate != null ? request.DueDate.ToDateTime() : null;
        return new UpdateTodo(title, description, dueDate);
    }

    public static TodoListMessage CreateTodoListMessage(this Todo todo)
    {
        var message = new TodoListMessage();
        message.Id = todo.Id.ToString();
        message.Title = todo.Title;
        message.Description = todo.Description;
        message.DueDate = todo.DueDate?.ToTimestamp();
        message.Completed = todo.IsCompleted;
        message.CreatedAt = todo.CreatedAt.ToTimestamp();
        message.UpdatedAt = todo.UpdatedAt.ToTimestamp();
        message.CompletedAt = todo.CompletedAt?.ToTimestamp();
        return message;
    }

    public static TodoMessage CreateTodoMessage(this Todo todo)
    {
        var message = new TodoMessage();
        message.Id = todo.Id.ToString();
        message.Title = todo.Title;
        message.Description = todo.Description;
        message.DueDate = todo.DueDate?.ToTimestamp();
        message.Completed = todo.IsCompleted;
        message.CreatedAt = todo.CreatedAt.ToTimestamp();
        message.UpdatedAt = todo.UpdatedAt.ToTimestamp();
        message.CompletedAt = todo.CompletedAt?.ToTimestamp();
        message.Items.AddRange(todo.Items.Select(i => i.CreateItemMessage()));
        return message;
    }

    public static ItemMessage CreateItemMessage(this Item item)
    {
        var message = new ItemMessage();
        message.Id = item.Id.ToString();
        message.Title = item.Title;
        message.Description = item.Description;
        message.DueDate = item.DueDate?.ToTimestamp();
        message.Completed = item.IsCompleted;
        message.CreatedAt = item.CreatedAt.ToTimestamp();
        message.UpdatedAt = item.UpdatedAt.ToTimestamp();
        message.CompletedAt = item.CompletedAt?.ToTimestamp();
        return message;
    }

    public static CreateItem ToCreateItemMessage(this AddItemMessage request)
    {
        var title = request.Title;
        var description = request.Description;
        DateTime? dueDate = request.DueDate != null ? request.DueDate.ToDateTime() : null;
        return new CreateItem(title, description, dueDate);
    }

    public static UpdateItem ToUpdateItemMessage(this UpdateItemMessage request)
    {
        var title = request.Title;
        var description = request.Description;
        DateTime? dueDate = request.DueDate != null ? request.DueDate.ToDateTime() : null;
        return new UpdateItem(title, description, dueDate);
    }
}