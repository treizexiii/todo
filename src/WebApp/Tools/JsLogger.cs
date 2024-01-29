using Microsoft.JSInterop;

namespace WebApp.Tools;

public class JsConsole<T>(IJSRuntime jsRuntime) :  IJsLogger<T>
{
    private readonly string _prefix = typeof(T).Name;

    public async Task LogInformation(string message)
    {
        await jsRuntime.InvokeVoidAsync("console.log", $"[{_prefix}]\n" + message);
    }

    public async Task LogWarning(string message)
    {
        await jsRuntime.InvokeVoidAsync("console.warn", $"[{_prefix}]\n" + message);
    }

    public async Task LogError(string message)
    {
        await jsRuntime.InvokeVoidAsync("console.error", $"[{_prefix}]\n" + message);
    }

    public async Task LogError(Exception exception)
    {
        await jsRuntime.InvokeVoidAsync("console.error", $"[{_prefix}]\n" + exception.Message);
    }

    // public async Task LogDebug(string message)
    // {
    //     await jsRuntime.InvokeVoidAsync("console.debug", $"[{_prefix}]\n" + message);
    // }
    //
    // public async Task LogDebug(object obj)
    // {
    //     await jsRuntime.InvokeVoidAsync("console.debug", $"[{_prefix}]\n" + obj);
    // }
}

public interface IJsLogger<T>
{
    Task LogInformation(string message);
    Task LogWarning(string message);
    Task LogError(string message);
    Task LogError(Exception exception);

    // Task LogDebug(string message);
    // Task LogDebug(object exception);
}

public static class ConsoleExtensions
{
    public static IServiceCollection AddConsole(this IServiceCollection services)
    {
        services.AddScoped(typeof(IJsLogger<>),typeof(JsConsole<>));
        return services;
    }
}