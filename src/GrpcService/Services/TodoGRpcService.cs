using Core.Dto;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcMessage;

// using CreateTodoRequest = GrpcMesssage.CreateTodoRequest;

namespace GrpcService.Services;

public class TodoGRpcService(ILogger<TodoGRpcService> logger, ITodoService service)
    : GrpcMessage.TodoService.TodoServiceBase
{
    public override async Task<Response> GetTodos(GetTodosRequest request, ServerCallContext context)
    {
        logger.LogInformation("Getting todos");
        try
        {
            var todos = await service.GetAllAsync(request.TodoFilterOptions());
            var messages = todos.Select(t => t.CreateTodoMessage());
            return ResponseFactory.CreateSuccessResponse(messages);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting todos");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> CreateTodo(CreateTodoMessage request,
        ServerCallContext context)
    {
        logger.LogInformation("Creating todo");
        try
        {
            var dto = request.ToCreateTodoRequest();
            var todo = await service.CreateAsync(dto);

            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating todo");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> UpdateTodo(UpdateTodoMessage request, ServerCallContext context)
    {
        logger.LogInformation("Updating todo");
        try
        {
            var todo = await service.UpdateAsync(
                Guid.Parse(request.Id),
                request.ToUpdateTodoRequest());
            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating todo");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> CompleteTodo(CompleteTodoMessage request, ServerCallContext context)
    {
        logger.LogInformation("Completing todo");
        try
        {
            var todo = await service.CompleteAsync(Guid.Parse(request.Id));
            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error completing todo");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }
}

public static class GrpcMessageExtensions
{
    public static TodoFilterOptions TodoFilterOptions(this GetTodosRequest request)
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
        return message;
    }
}
