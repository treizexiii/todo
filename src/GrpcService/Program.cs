using Core;
using GrpcService.Services;
using Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.MaxReceiveMessageSize = 2 * 1024 * 1024; // 2 MB
    options.MaxSendMessageSize = 5 * 1024 * 1024; // 5 MB
});
    // .AddJsonTranscoding(configure =>
    // {
    //     configure.JsonSettings.WriteIndented = true;
    //     configure.JsonSettings.IgnoreDefaultValues = true;
    // });
builder.Services.AddInMemoryContext("TodoDb");
builder.Services.AddCore();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(setup => { setup.SwaggerDoc("v1", new() { Title = "Todo API", Version = "v1" }); });
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseEndpoints(builder =>
{
    builder.MapGrpcService<TodoGRpcService>();
    builder.MapGrpcService<ItemGRpcService>();
});

// app.MapGrpcService<TodoGRpcService>().EnableGrpcWeb();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1"));

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();