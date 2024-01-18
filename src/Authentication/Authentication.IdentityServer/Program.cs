using Authentication.Persistence;
using Authentication.Persistence.Database;
using Authentication.Services;
using Database;
using Database.Context;
using Tools.TransactionManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

builder.Services.AddIdentityDb(builder.Configuration.GetConnectionString("IdentityDb") ??
                               throw new InvalidOperationException("IdentityDb connection string is null"));

builder.Services.AddPostgresContext(builder.Configuration.GetConnectionString("TodoDb") ??
                                    throw new InvalidOperationException("TodoDb connection string is null"));

builder.Services.AddServices();

builder.Services.AddMultiContextTransactionManager([
    typeof(IdentityDb), typeof(TodoDb)
]);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.MapControllers();

Console.WriteLine(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// var transaction = app.Services.GetRequiredService<Tools.TransactionManager.ITransactionManager>();

app.Run();