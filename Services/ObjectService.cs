using System.Text.Json;

namespace CitizensApiV2.Services;

public class ObjectService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly ILogger<ObjectService> _logger;

    public ObjectService(
        HttpClient http,
        IConfiguration config,
        ILogger<ObjectService> logger)
    {
        _http = http;
        _config = config;
        _logger = logger;
    }

    public async Task<string> GetRandomAsset()
    {
        var url = _config["AppSettings:ObjectsApiUrl"];

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new Exception("Objects API URL is not configured");
        }

        _logger.LogInformation("External API request executed");

        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonDocument.Parse(json).RootElement;

        var list = data.EnumerateArray().ToList();

        if (list.Count == 0)
        {
            return "Unknown Asset";
        }

        var random = new Random();
        var selected = list[random.Next(list.Count)];

        if (selected.TryGetProperty("name", out var nameProperty))
        {
            return nameProperty.GetString() ?? "Unknown Asset";
        }

        return "Unknown Asset";
    }
}