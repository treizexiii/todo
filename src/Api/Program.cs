using Core;
using Database;
using Database.Context;
using Tools.TransactionManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddPostgresContext(builder.Configuration.GetConnectionString("TodoDb") ??
                                    throw new InvalidOperationException("IdentityDb connection string is null"));

builder.Services.AddTransactionManager<TodoDb>();

builder.Services.AddCore();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(setup => { setup.AddDefaultPolicy(policyBuilder => { policyBuilder.AllowAnyOrigin(); }); });

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();