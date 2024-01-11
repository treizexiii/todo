using Core.Services;
using Grpc.Core;
using GrpcMessage;
using GrpcMessage.Todo;

namespace GrpcService.Services;

public class TodoGRpcService(ILogger<TodoGRpcService> logger, ITodoService service)
    : GrpcMessage.Todo.TodoService.TodoServiceBase
{
    public override async Task<Response> GetTodos(TodosFilterMessage request, ServerCallContext context)
    {
        logger.LogInformation("Getting todos");
        try
        {
            var todos = await service.GetAllAsync(request.TodoFilterOptions());
            var messages = todos.Select(t => t.CreateTodoListMessage());
            return ResponseFactory.CreateSuccessResponse(messages);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting todos");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }

    public override async Task<Response> GetTodo(IdMessage request, ServerCallContext context)
    {
        logger.LogInformation("Getting todo");
        try
        {
            var todo = await service.GetAsync(Guid.Parse(request.Id));
            if (todo is null)
            {
                return ResponseFactory.CreateErrorResponse(404, $"Todo with id {request.Id} not found");
            }
            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting todo");
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

            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoListMessage());
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
            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoListMessage());
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
            return ResponseFactory.CreateSuccessResponse(todo.CreateTodoListMessage());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error completing todo");
            return ResponseFactory.CreateErrorResponse(500, e.Message);
        }
    }
}