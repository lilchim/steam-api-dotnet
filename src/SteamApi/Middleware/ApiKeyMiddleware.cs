using Microsoft.Extensions.Options;
using SteamApi.Configuration;
using System.Collections.Concurrent;

namespace SteamApi.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ApiKeyOptions _options;
    private readonly ILogger<ApiKeyMiddleware> _logger;
    private readonly ConcurrentDictionary<string, RateLimitInfo> _rateLimitStore = new();

    public ApiKeyMiddleware(RequestDelegate next, IOptions<ApiKeyOptions> options, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _options = options.Value;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip API key validation for certain endpoints
        if (ShouldSkipApiKeyValidation(context))
        {
            await _next(context);
            return;
        }

        // If API key is not required, skip validation
        if (!_options.RequireApiKey)
        {
            await _next(context);
            return;
        }

        var apiKey = GetApiKey(context);

        if (string.IsNullOrEmpty(apiKey))
        {
            _logger.LogWarning("API key missing from request to {Path}", context.Request.Path);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "API key is required" });
            return;
        }

        if (!IsValidApiKey(apiKey))
        {
            _logger.LogWarning("Invalid API key provided for request to {Path}", context.Request.Path);
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Invalid API key" });
            return;
        }

        // Rate limiting
        if (_options.RateLimit.Enabled && !CheckRateLimit(apiKey))
        {
            _logger.LogWarning("Rate limit exceeded for API key {ApiKey}", MaskApiKey(apiKey));
            context.Response.StatusCode = 429;
            await context.Response.WriteAsJsonAsync(new { error = "Rate limit exceeded" });
            return;
        }

        // Add API key to context for logging/auditing
        context.Items["ApiKey"] = MaskApiKey(apiKey);

        await _next(context);
    }

    private bool ShouldSkipApiKeyValidation(HttpContext context)
    {
        // Skip API key validation for health checks and status endpoints
        var path = context.Request.Path.Value?.ToLower();
        return path?.StartsWith("/api/status") == true || 
               path?.StartsWith("/health") == true ||
               path?.StartsWith("/swagger") == true;
    }

    private string? GetApiKey(HttpContext context)
    {
        // Try header first
        if (context.Request.Headers.TryGetValue(_options.HeaderName, out var headerValue))
        {
            return headerValue.FirstOrDefault();
        }

        // Try query parameter as fallback
        if (context.Request.Query.TryGetValue(_options.QueryParameterName, out var queryValue))
        {
            return queryValue.FirstOrDefault();
        }

        return null;
    }

    private bool IsValidApiKey(string apiKey)
    {
        return _options.ValidApiKeys.Contains(apiKey);
    }

    private bool CheckRateLimit(string apiKey)
    {
        var now = DateTime.UtcNow;
        var rateLimitInfo = _rateLimitStore.GetOrAdd(apiKey, _ => new RateLimitInfo());

        // Clean up old entries
        rateLimitInfo.CleanupOldRequests(now);

        // Check minute limit
        if (rateLimitInfo.RequestsPerMinute.Count >= _options.RateLimit.RequestsPerMinute)
        {
            return false;
        }

        // Check hour limit
        if (rateLimitInfo.RequestsPerHour.Count >= _options.RateLimit.RequestsPerHour)
        {
            return false;
        }

        // Add current request
        rateLimitInfo.RequestsPerMinute.Add(now);
        rateLimitInfo.RequestsPerHour.Add(now);

        return true;
    }

    private static string MaskApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey) || apiKey.Length <= 8)
            return "***";

        return apiKey[..4] + "..." + apiKey[^4..];
    }
}

public class RateLimitInfo
{
    public List<DateTime> RequestsPerMinute { get; } = new();
    public List<DateTime> RequestsPerHour { get; } = new();

    public void CleanupOldRequests(DateTime now)
    {
        var oneMinuteAgo = now.AddMinutes(-1);
        var oneHourAgo = now.AddHours(-1);

        RequestsPerMinute.RemoveAll(time => time < oneMinuteAgo);
        RequestsPerHour.RemoveAll(time => time < oneHourAgo);
    }
} 