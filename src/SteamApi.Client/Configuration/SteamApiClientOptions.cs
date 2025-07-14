namespace SteamApi.Client.Configuration;

/// <summary>
/// Configuration options for the Steam API client
/// </summary>
public class SteamApiClientOptions
{
    /// <summary>
    /// Base URL of the Steam API service
    /// </summary>
    public string BaseUrl { get; set; } = "http://localhost:5000";

    /// <summary>
    /// API key for authentication (optional)
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// HTTP request timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Maximum number of retry attempts
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Whether to enable request/response logging
    /// </summary>
    public bool EnableLogging { get; set; } = false;

    /// <summary>
    /// User agent string for HTTP requests
    /// </summary>
    public string UserAgent { get; set; } = "SteamApi.Client/1.0.0";

    /// <summary>
    /// Cache configuration
    /// </summary>
    public CacheOptions Cache { get; set; } = new();
}

/// <summary>
/// Cache configuration options
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// Whether caching is enabled
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Cache type to use
    /// </summary>
    public CacheType Type { get; set; } = CacheType.Memory;

    /// <summary>
    /// Default time-to-live for cached items in minutes
    /// </summary>
    public int DefaultTtlMinutes { get; set; } = 15;

    /// <summary>
    /// Redis connection string (required if Type is Redis)
    /// </summary>
    public string? RedisConnectionString { get; set; }
}

/// <summary>
/// Supported cache types
/// </summary>
public enum CacheType
{
    /// <summary>
    /// No caching
    /// </summary>
    None,

    /// <summary>
    /// In-memory caching
    /// </summary>
    Memory,

    /// <summary>
    /// Redis caching
    /// </summary>
    Redis
} 