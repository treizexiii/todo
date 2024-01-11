// See https://aka.ms/new-console-template for more information

using GrpcClient;
using GrpcMessage;

Console.WriteLine("Hello, World!");

// using var channel = GrpcChannel.ForAddress("http://localhost:8080");
var proxy = new TodoServiceProxy("http://localhost:8080");

var todo1 = new CreateTodoMessage();
todo1.Title = "Test";
todo1.Description = "Test";
todo1.DueDate = null;
await proxy.CreateAsync(todo1);

var todo2 = new CreateTodoMessage();
todo2.Title = "Test2";
todo2.Description = "Test2";
todo2.DueDate = null;
await proxy.CreateAsync(todo2);

var todo3 = new CreateTodoMessage();
todo3.Title = "Test3";
todo3.Description = "Test3";
todo3.DueDate = null;
await proxy.CreateAsync(todo3);


var todos = await proxy.GetAllAsync();
foreach (var todo in todos)
{
    Console.WriteLine(todo.Title);
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();