namespace Api.Wrappers;

public class ApiResponse
{
    public string Version { get; set; } = string.Empty;
    public int Code { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; } = default!;
}