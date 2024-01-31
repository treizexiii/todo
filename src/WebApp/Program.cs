using Client.HttpRestClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using WebApp.Services;
using WebApp.Tools;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var apiUrl = builder.Configuration.GetValue<string>("API_URL")
    ?? throw new Exception("ApiUrl not found in appsettings.json");

builder.Services.AddRestClient(apiUrl);
builder.Services.AddScoped<TodoService>();

builder.Services.AddConsole();

var app = builder.Build();

await app.RunAsync();