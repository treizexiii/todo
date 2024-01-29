using Microsoft.JSInterop;

namespace WebApp.Tools;

public abstract class AbstractJsConsole : IJsLogger
{
    protected readonly IJSRuntime _jsRuntime;

    public AbstractJsConsole(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public abstract void LogInformation(string message);

    public abstract void LogWarning(string message);

    public  abstract void LogError(string message);
}

public class JsConsole<T> : AbstractJsConsole, IJsLogger<T>
{
    private readonly string _prefix;

    public JsConsole(IJSRuntime jsRuntime) : base(jsRuntime)
    {
        _prefix = typeof(T).Name;
    }

    public override void LogInformation(string message)
    {
        _jsRuntime.InvokeVoidAsync("console.log", message);
    }

    public override void LogWarning(string message)
    {
        _jsRuntime.InvokeVoidAsync("console.warn", message);
    }

    public override void LogError(string message)
    {
        _jsRuntime.InvokeVoidAsync("console.error", message);
    }
}

public interface IJsLogger
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
}

public interface IJsLogger<T> : IJsLogger
{
}

public static class ConsoleExtensions
{
    public static IServiceCollection AddConsole(this IServiceCollection services)
    {
        services.AddScoped<IJsLogger, AbstractJsConsole>();
        return services;
    }
}
