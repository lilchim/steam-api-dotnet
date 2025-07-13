using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamNewsController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<SteamNewsController> _logger;

    public SteamNewsController(ISteamApiService steamApi, ILogger<SteamNewsController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get news articles for a specific Steam app
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <param name="count">Number of news items to return (max 20)</param>
    /// <param name="maxLength">Maximum length of each news item (0 for no limit)</param>
    /// <param name="feeds">Comma-separated list of feed names to return news from</param>
    /// <param name="tags">Comma-separated list of tags to filter by</param>
    /// <returns>News articles for the specified app</returns>
    [HttpGet("app/{appId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetNewsForApp(
        int appId,
        [FromQuery] int count = 20,
        [FromQuery] int maxLength = 0,
        [FromQuery] string? feeds = null,
        [FromQuery] string? tags = null)
    {
        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        if (count <= 0 || count > 20)
        {
            return BadRequest("Count must be between 1 and 20");
        }

        if (maxLength < 0)
        {
            return BadRequest("Max length must be 0 or greater");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["appid"] = appId.ToString(),
                ["count"] = count.ToString(),
                ["maxlength"] = maxLength.ToString()
            };

            if (!string.IsNullOrEmpty(feeds))
            {
                parameters["feeds"] = feeds;
            }

            if (!string.IsNullOrEmpty(tags))
            {
                parameters["tags"] = tags;
            }

            _logger.LogInformation("Getting news for app {AppId} with count {Count}", appId, count);
            
            var response = await _steamApi.GetAsync("ISteamNews", "GetNewsForApp", "v0002", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get news for app {AppId}", appId);
            return StatusCode(500, "Failed to retrieve news from Steam API");
        }
    }

    /// <summary>
    /// Get news articles for a specific Steam app with additional features (requires API key)
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <param name="count">Number of news items to return (max 20)</param>
    /// <param name="maxLength">Maximum length of each news item (0 for no limit)</param>
    /// <param name="feeds">Comma-separated list of feed names to return news from</param>
    /// <param name="tags">Comma-separated list of tags to filter by</param>
    /// <param name="endDate">End date for news items (Unix timestamp)</param>
    /// <param name="days">Number of days to look back for news items</param>
    /// <returns>News articles for the specified app with additional features</returns>
    [HttpGet("app/{appId}/authed")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetNewsForAppAuthed(
        int appId,
        [FromQuery] int count = 20,
        [FromQuery] int maxLength = 0,
        [FromQuery] string? feeds = null,
        [FromQuery] string? tags = null,
        [FromQuery] long? endDate = null,
        [FromQuery] int? days = null)
    {
        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        if (count <= 0 || count > 20)
        {
            return BadRequest("Count must be between 1 and 20");
        }

        if (maxLength < 0)
        {
            return BadRequest("Max length must be 0 or greater");
        }

        if (days.HasValue && (days.Value <= 0 || days.Value > 365))
        {
            return BadRequest("Days must be between 1 and 365");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["appid"] = appId.ToString(),
                ["count"] = count.ToString(),
                ["maxlength"] = maxLength.ToString()
            };

            if (!string.IsNullOrEmpty(feeds))
            {
                parameters["feeds"] = feeds;
            }

            if (!string.IsNullOrEmpty(tags))
            {
                parameters["tags"] = tags;
            }

            if (endDate.HasValue)
            {
                parameters["enddate"] = endDate.Value.ToString();
            }

            if (days.HasValue)
            {
                parameters["days"] = days.Value.ToString();
            }

            _logger.LogInformation("Getting authenticated news for app {AppId} with count {Count}", appId, count);
            
            var response = await _steamApi.GetAsync("ISteamNews", "GetNewsForAppAuthed", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get authenticated news for app {AppId}", appId);
            return StatusCode(500, "Failed to retrieve news from Steam API");
        }
    }
} 