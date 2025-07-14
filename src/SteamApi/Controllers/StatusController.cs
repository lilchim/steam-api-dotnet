using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SteamApi.Configuration;
using SteamApi.Models;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly SteamApiOptions _steamOptions;
    private readonly ApiKeyOptions _apiKeyOptions;
    private readonly ILogger<StatusController> _logger;

    public StatusController(
        IOptions<SteamApiOptions> steamOptions, 
        IOptions<ApiKeyOptions> apiKeyOptions,
        ILogger<StatusController> logger)
    {
        _steamOptions = steamOptions.Value;
        _apiKeyOptions = apiKeyOptions.Value;
        _logger = logger;
    }

    /// <summary>
    /// Get the current status and configuration of the Steam API service
    /// </summary>
    /// <returns>Status information including configuration details</returns>
    [HttpGet]
    [ProducesResponseType(typeof(StatusResponse), 200)]
    public ActionResult<StatusResponse> GetStatus()
    {
        _logger.LogInformation("Status endpoint called");
        
        var response = new StatusResponse
        {
            SteamApiKeyConfigured = !string.IsNullOrEmpty(_steamOptions.ApiKey),
            ApiKeyAuthEnabled = _apiKeyOptions.RequireApiKey,
            RateLimitEnabled = _apiKeyOptions.RateLimit.Enabled,
            BaseUrl = _steamOptions.BaseUrl,
            TimeoutSeconds = _steamOptions.TimeoutSeconds,
            MaxRetries = _steamOptions.MaxRetries,
            EnableLogging = _steamOptions.EnableLogging,
            Version = GetApplicationVersion(),
            Timestamp = DateTime.UtcNow,
            Status = "Healthy"
        };

        return Ok(response);
    }

    /// <summary>
    /// Simple health check endpoint
    /// </summary>
    /// <returns>Health status</returns>
    [HttpGet("health")]
    [ProducesResponseType(200)]
    public ActionResult Health()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }

    private static string GetApplicationVersion()
    {
        // For now, return a simple version. In a real app, you might get this from assembly info
        return typeof(StatusController).Assembly.GetName().Version?.ToString() ?? "1.0.0";
    }
} 