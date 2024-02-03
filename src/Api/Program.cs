using Api.Tools;
using AspExtension;
using Core;
using Persistence.Database;
using Persistence.Database.Context;
using Tools.TransactionManager;


namespace Api;

internal static class Program
{
    private const string CONFIG_PATH = "Configuration";

    private static readonly string AspEnvironment =
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.ConfigureHost(CONFIG_PATH, AspEnvironment);
        builder.AddServices();
        var app = builder.Build();
        app.StarterLog();
        app.ConfigureApp();
        app.Run();
    }

    // Add services to the container.
    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddCors(setup =>
        {
            setup.AddPolicy("*", policyBuilder =>
            {
                policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddPostgresContext(builder.Configuration.BuildPostgresConnectionString());

        builder.Services.AddTransactionManager<TodoDb>();

        builder.Services.AddCore();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void ConfigureApp(this WebApplication app)
    {
        app.UseCors("*");
        app.UseRouting();
        app.MapControllers();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
    }
}