using Microsoft.Extensions.Hosting;

namespace Persistence.MigrationTool;

public class App
{
    private readonly IHost _host;

    public App(IHost host)
    {
        _host = host;
    }

    public async Task RunAsync(string[] args)
    {

    }
}

public class Log<T>
{
    private Type _type;

    public Log()
    {
        _type = typeof(T);
    }

    public void LogInformation(string message)
    {
        Console.WriteLine($"[{_type.Name}] {message}");
    }
}