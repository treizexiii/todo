namespace TodoClient.Wrapper;

public class Response
{
    public string Version { get; set; } = string.Empty;
    public int Code { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}

public class Response<T> : Response
{
    public T Data { get; set; } = default!;
}