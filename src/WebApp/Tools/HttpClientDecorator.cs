using System.Net.Http.Headers;

namespace WebApp.Tools;

public class HttpClientDecorator(HttpClient httpClient)
{
    private bool _isJwtTokenSet;

    public HttpClient HttpClient()
    {
        return httpClient;
    }

    public bool IsJwtTokenSet()
    {
        return _isJwtTokenSet;
    }

    public void SetJwtToken(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        _isJwtTokenSet = true;
    }
}