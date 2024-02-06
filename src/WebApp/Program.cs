using Client.HttpRestClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp.Services;
using WebApp.Tools;

namespace WebApp;

internal static class Program
{
    private const string CONFIG_PATH = "configuration";

    private static readonly string AppEnvironment =
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "None";

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Configuration.AddEnvironmentVariables();

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

#if DEBUG
        builder.Services.AddSingleton(
            _ => new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:{5000}"),
                DefaultRequestHeaders =
                {

                }
            });
#else
        builder.Services.AddSingleton(
            _ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
#endif
        builder.Services.AddRestClient();
        builder.Services.AddScoped<TodoServiceFacade>();

        builder.Services.AddConsole();

        var app = builder.Build();

        await app.RunAsync();
    }

    private static async Task AddConfiguration(WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
        builder.Services.AddScoped(_ => http);
        // using var response = await http.GetAsync($"{CONFIG_PATH}/appsettings.json");
        using var response = await http.GetAsync($"{CONFIG_PATH}/appsettings.{AppEnvironment}.json");
        await using var stream = await response.Content.ReadAsStreamAsync();
        // await using var stream2 = await http.GetStreamAsync($"{CONFIG_PATH}/appsettings.{AppEnvironment}.json");
        builder.Configuration.AddJsonStream(stream);
        // builder.Configuration.AddJsonStream(stream2);
    }
}