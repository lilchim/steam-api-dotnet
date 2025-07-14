namespace SteamApi.Configuration;

public class CorsOptions
{
    public const string SectionName = "Cors";

    /// <summary>
    /// Whether CORS is enabled
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Allowed origins (domains that can access the API)
    /// </summary>
    public List<string> AllowedOrigins { get; set; } = new();

    /// <summary>
    /// Allowed HTTP methods
    /// </summary>
    public List<string> AllowedMethods { get; set; } = new() { "GET", "POST", "PUT", "DELETE", "OPTIONS" };

    /// <summary>
    /// Allowed headers
    /// </summary>
    public List<string> AllowedHeaders { get; set; } = new() { "Content-Type", "X-API-Key", "Authorization" };

    /// <summary>
    /// Whether to allow credentials (cookies, authorization headers)
    /// </summary>
    public bool AllowCredentials { get; set; } = false;

    /// <summary>
    /// How long to cache preflight requests (in seconds)
    /// </summary>
    public int PreflightMaxAge { get; set; } = 86400; // 24 hours

    /// <summary>
    /// Whether to expose response headers to the client
    /// </summary>
    public List<string> ExposedHeaders { get; set; } = new();
} 