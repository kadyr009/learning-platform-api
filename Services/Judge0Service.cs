using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LearningPlatformAPI.Services;

public class Judge0Service
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public Judge0Service(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string?> RunCodeAsync(string sourceCode)
    {
        var requestBody = new
        {
            source_code = sourceCode,
            language_id = 51 // C#
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var apiKey = _config["Judge0:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "judge0-ce.p.rapidapi.com");

        var response = await _httpClient.PostAsync(
            "https://judge0-ce.p.rapidapi.com/submissions?base64_encoded=false&wait=true",
            content
        );

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.TryGetProperty("stdout", out var stdout))
        {
            return stdout.GetString();
        }

        return null;
    }
}
