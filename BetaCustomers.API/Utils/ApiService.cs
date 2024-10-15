namespace BetaCustomers.API.Utils;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> PostDataWithCustomHeaderAsync(string url, string jsonData, string key)
    {
        // Create a new  HttpRequestMessage
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json")
        };
        
        // Add custom headers
        request.Headers.Add("X-API-KEY", key);
        
        // Send the request
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        // Read and return response content
        return await response.Content.ReadAsStringAsync();
    }

}