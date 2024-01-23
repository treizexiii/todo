using Client.HttpRestClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using WebApp.Services;

// const string grpcUrl = "http://todo-api:5000";
const string grpcUrl = "http://192.168.1.65:5000";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddRestClient(grpcUrl);
builder.Services.AddScoped<TodoService>();

var app = builder.Build();

await app.RunAsync();