namespace SteamApi.Configuration;

public class SteamApiOptions
{
    public const string SectionName = "SteamApi";
    
    /// <summary>
    /// Steam Web API Key. Required for all API calls.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Base URL for Steam Web API. Defaults to https://api.steampowered.com
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.steampowered.com";
    
    /// <summary>
    /// Base URL for Steam Store API. Defaults to https://store.steampowered.com/api
    /// </summary>
    public string StoreBaseUrl { get; set; } = "https://store.steampowered.com/api";
    
    /// <summary>
    /// Request timeout in seconds. Defaults to 30 seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Maximum number of retry attempts for failed requests. Defaults to 3.
    /// </summary>
    public int MaxRetries { get; set; } = 3;
    
    /// <summary>
    /// Whether to enable request/response logging. Defaults to false.
    /// </summary>
    public bool EnableLogging { get; set; } = false;
} 