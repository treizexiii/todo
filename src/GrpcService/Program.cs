using Core;
using Database;
using GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc()
    .AddJsonTranscoding(configure =>
    {
        configure.JsonSettings.WriteIndented = true;
        configure.JsonSettings.IgnoreDefaultValues = true;
    });
builder.Services.AddInMemoryContext("TodoDb");
builder.Services.AddCore();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(setup => { setup.SwaggerDoc("v1", new() { Title = "Todo API", Version = "v1" }); });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.MapGrpcService<TodoGRpcService>().EnableGrpcWeb();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1"));

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();