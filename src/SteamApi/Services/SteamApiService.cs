using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SteamApi.Configuration;

namespace SteamApi.Services;

/// <summary>
/// Implementation of the Steam API service for making HTTP calls to the Steam Web API
/// </summary>
public class SteamApiService : ISteamApiService
{
    private readonly HttpClient _httpClient;
    private readonly SteamApiOptions _options;
    private readonly ILogger<SteamApiService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public SteamApiService(
        HttpClient httpClient,
        IOptions<SteamApiOptions> options,
        ILogger<SteamApiService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
        
        // TODO: Steam's API supports XML and...something else. Could make that configurable.
        // Configure JSON serialization options
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<string> GetAsync(string interfaceName, string methodName, string version, Dictionary<string, string>? parameters = null)
    {
        var url = BuildSteamApiUrl(interfaceName, methodName, version, parameters);
        
        if (_options.EnableLogging)
        {
            _logger.LogInformation("Making Steam API request to: {Url}", url);
        }

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            
            if (_options.EnableLogging)
            {
                _logger.LogInformation("Steam API response received successfully");
            }
            
            return content;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for Steam API call: {Interface}/{Method}/{Version}", 
                interfaceName, methodName, version);
            throw new SteamApiException($"Steam API request failed: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Steam API request timed out: {Interface}/{Method}/{Version}", 
                interfaceName, methodName, version);
            throw new SteamApiException("Steam API request timed out", ex);
        }
    }

    public async Task<T> GetAsync<T>(string interfaceName, string methodName, string version, Dictionary<string, string>? parameters = null)
    {
        var jsonResponse = await GetAsync(interfaceName, methodName, version, parameters);
        
        try
        {
            var result = JsonSerializer.Deserialize<T>(jsonResponse, _jsonOptions);
            if (result == null)
            {
                throw new SteamApiException("Failed to deserialize Steam API response");
            }
            return result;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize Steam API response for {Interface}/{Method}/{Version}", 
                interfaceName, methodName, version);
            throw new SteamApiException("Failed to deserialize Steam API response", ex);
        }
    }

    public async Task<string> GetStoreAsync(string endpoint, Dictionary<string, string>? parameters = null)
    {
        var url = BuildStoreApiUrl(endpoint, parameters);
        
        if (_options.EnableLogging)
        {
            _logger.LogInformation("Making Steam Store API request to: {Url}", url);
        }

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            
            if (_options.EnableLogging)
            {
                _logger.LogInformation("Steam Store API response received successfully");
            }
            
            return content;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for Steam Store API call: {Endpoint}", endpoint);
            throw new SteamApiException($"Steam Store API request failed: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Steam Store API request timed out: {Endpoint}", endpoint);
            throw new SteamApiException("Steam Store API request timed out", ex);
        }
    }

    public async Task<T> GetStoreAsync<T>(string endpoint, Dictionary<string, string>? parameters = null)
    {
        var jsonResponse = await GetStoreAsync(endpoint, parameters);
        
        try
        {
            var result = JsonSerializer.Deserialize<T>(jsonResponse, _jsonOptions);
            if (result == null)
            {
                throw new SteamApiException("Failed to deserialize Steam Store API response");
            }
            return result;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize Steam Store API response for {Endpoint}", endpoint);
            throw new SteamApiException("Failed to deserialize Steam Store API response", ex);
        }
    }

    private string BuildSteamApiUrl(string interfaceName, string methodName, string version, Dictionary<string, string>? parameters)
    {
        // Steam API URL format: http://api.steampowered.com/<interface>/<method>/<version>/?key=<api_key>&format=json&<other_params>
        var url = $"{_options.BaseUrl.TrimEnd('/')}/{interfaceName}/{methodName}/{version}/";
        
        var queryParams = new List<string>
        {
            $"key={Uri.EscapeDataString(_options.ApiKey)}",
            "format=json"
        };

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                queryParams.Add($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}");
            }
        }

        return $"{url}?{string.Join("&", queryParams)}";
    }

    private string BuildStoreApiUrl(string endpoint, Dictionary<string, string>? parameters)
    {
        // Steam Store API URL format: https://store.steampowered.com/api/<endpoint>?<params>
        var url = $"{_options.StoreBaseUrl.TrimEnd('/')}/{endpoint}";
        
        var queryParams = new List<string>();

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                queryParams.Add($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}");
            }
        }

        return queryParams.Count > 0 ? $"{url}?{string.Join("&", queryParams)}" : url;
    }
}

/// <summary>
/// Custom exception for Steam API errors
/// </summary>
public class SteamApiException : Exception
{
    public SteamApiException(string message) : base(message) { }
    public SteamApiException(string message, Exception innerException) : base(message, innerException) { }
} 