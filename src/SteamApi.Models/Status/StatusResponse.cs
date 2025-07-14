namespace SteamApi.Models.Status;

/// <summary>
/// Response model for the API status endpoint
/// </summary>
public class StatusResponse
{
    /// <summary>
    /// Whether the Steam API key is configured
    /// </summary>
    public bool SteamApiKeyConfigured { get; set; }
    
    /// <summary>
    /// Whether API key authentication is enabled
    /// </summary>
    public bool ApiKeyAuthEnabled { get; set; }
    
    /// <summary>
    /// Whether rate limiting is enabled
    /// </summary>
    public bool RateLimitEnabled { get; set; }
    
    /// <summary>
    /// Whether CORS is enabled
    /// </summary>
    public bool CorsEnabled { get; set; }
    
    /// <summary>
    /// Base URL for Steam Web API
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Request timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; }
    
    /// <summary>
    /// Maximum number of retry attempts
    /// </summary>
    public int MaxRetries { get; set; }
    
    /// <summary>
    /// Whether request/response logging is enabled
    /// </summary>
    public bool EnableLogging { get; set; }
    
    /// <summary>
    /// Application version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Current timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Application status
    /// </summary>
    public string Status { get; set; } = "Healthy";
} 