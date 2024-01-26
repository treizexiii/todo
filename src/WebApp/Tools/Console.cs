using Microsoft.JSInterop;

namespace WebApp.Tools;

public class Console(IJSRuntime jsRuntime)
{
    public async Task Log(string message)
    {
        await jsRuntime.InvokeVoidAsync("console.log", message);
    }

    public async Task Log(object message)
    {
        await jsRuntime.InvokeVoidAsync("console.log", message);
    }

    public async Task Error(string message)
    {
        await jsRuntime.InvokeVoidAsync("console.error", message);
    }

    public async Task Error(object message)
    {
        await jsRuntime.InvokeVoidAsync("console.error", message);
    }
}

