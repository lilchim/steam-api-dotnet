namespace SteamApi.Configuration;

public class ApiKeyOptions
{
    public const string SectionName = "ApiKey";

    /// <summary>
    /// List of valid API keys (for development/testing)
    /// In production, these would be managed by your auth service
    /// </summary>
    public List<string> ValidApiKeys { get; set; } = new();

    /// <summary>
    /// Whether API key authentication is required
    /// </summary>
    public bool RequireApiKey { get; set; } = true;

    /// <summary>
    /// Header name to look for the API key (default: X-API-Key)
    /// </summary>
    public string HeaderName { get; set; } = "X-API-Key";

    /// <summary>
    /// Query parameter name as alternative to header (default: api_key)
    /// </summary>
    public string QueryParameterName { get; set; } = "api_key";

    /// <summary>
    /// Rate limiting settings per API key
    /// </summary>
    public RateLimitOptions RateLimit { get; set; } = new();
}

public class RateLimitOptions
{
    /// <summary>
    /// Maximum requests per minute per API key
    /// </summary>
    public int RequestsPerMinute { get; set; } = 100;

    /// <summary>
    /// Maximum requests per hour per API key
    /// </summary>
    public int RequestsPerHour { get; set; } = 1000;

    /// <summary>
    /// Whether rate limiting is enabled
    /// </summary>
    public bool Enabled { get; set; } = true;
} 