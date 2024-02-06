using System.Reflection;
using Core;
using Persistence.Database;
using Persistence.Database.Context;
using Tools.TransactionManager;
using static System.Console;

namespace Api.Tools;

public static class WebApplicationExtension
{
    public static void StarterLog(this IApplicationBuilder app)
    {
        // var logger = app.ApplicationServices.GetRequiredService<ILogger<WebApplication>>();
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        WriteLine("Application started");
        WriteLine("Environment: {0}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        WriteLine("Application name: {0}", Assembly.GetEntryAssembly()?.GetName().Name);
        WriteLine("Application version: {0}", Assembly.GetEntryAssembly()?.GetName().Version);
        WriteLine("Application started at: {0:o}", DateTime.UtcNow);
        WriteLine("Database name: {0}", configuration["DbSecret:Database"]);
        WriteLine("Database server: {0}", configuration["DbSecret:Host"]);
    }

    // Add services to the container.
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddCors(setup =>
        {
            setup.AddPolicy("*", policyBuilder =>
            {
                policyBuilder
                    // .WithOrigins("http://todos.treize.cloud")
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

    public static void ConfigureApp(this WebApplication app)
    {
        // app.UseCors("*");
        app.UseCors();
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