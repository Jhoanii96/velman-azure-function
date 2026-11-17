using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
public class FastFieldHttpService
{
    private readonly HttpClient _httpClient;

    public FastFieldHttpService(HttpClient httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public string MakeRequestAuthenticate(string url, string applicationId, string password, List<Header> headers)
    {
        try
        {
            return _MakeRequestAuthenticate(url, applicationId, password, headers).Result;
        }
        catch (AggregateException ex) when (ex.InnerException != null)
        {
            throw ex.InnerException;
        }
    }

    private async Task<string> _MakeRequestAuthenticate(string url, string applicationId, string password, List<Header> headers)
    {
        // Create basic authentication header
        string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                           .GetBytes($"{applicationId}:{password}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

        // Add custom headers
        if (headers?.Count > 0)
        {
            foreach (var item in headers)
            {
                if (!_httpClient.DefaultRequestHeaders.Contains(item.Name))
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Name, item.Value);
                }
            }
        }

        // Request body
        var requestContent = new StringContent("{body}", Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(url, requestContent);
            response.EnsureSuccessStatusCode(); // Throws exception if not successful

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP request failed: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("Request timed out", ex);
        }
    }
}


*/