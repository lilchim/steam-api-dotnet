using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;
using SteamApi.Models.Steam.Responses;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamUserStatsController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<SteamUserStatsController> _logger;

    public SteamUserStatsController(ISteamApiService steamApi, ILogger<SteamUserStatsController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get global achievement percentages for a specific Steam app
    /// </summary>
    /// <param name="gameId">Steam App ID</param>
    /// <returns>Global achievement completion percentages</returns>
    [HttpGet("achievements/{gameId}/global")]
    [ProducesResponseType(typeof(SteamResponse<AchievementPercentagesResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<AchievementPercentagesResponse>>> GetGlobalAchievementPercentagesForApp(int gameId)
    {
        if (gameId <= 0)
        {
            return BadRequest("Game ID must be a positive integer");
        }

        try
        {
            _logger.LogInformation("Getting global achievement percentages for game {GameId}", gameId);
            
            var response = await _steamApi.GetAsync("ISteamUserStats", "GetGlobalAchievementPercentagesForApp", "v0002", 
                new Dictionary<string, string> { ["gameid"] = gameId.ToString() });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get global achievement percentages for game {GameId}", gameId);
            return StatusCode(500, "Failed to retrieve achievement data from Steam API");
        }
    }



    /// <summary>
    /// Get the number of current players for a specific Steam app
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <returns>Current player count for the specified app</returns>
    [HttpGet("players/{appId}/current")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetNumberOfCurrentPlayers(int appId)
    {
        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        try
        {
            _logger.LogInformation("Getting current player count for app {AppId}", appId);
            
            var response = await _steamApi.GetAsync("ISteamUserStats", "GetNumberOfCurrentPlayers", "v0001", 
                new Dictionary<string, string> { ["appid"] = appId.ToString() });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get current player count for app {AppId}", appId);
            return StatusCode(500, "Failed to retrieve player count from Steam API");
        }
    }

    /// <summary>
    /// Get achievements for a specific user and game
    /// </summary>
    /// <param name="steamId">Steam ID of the user</param>
    /// <param name="appId">Steam App ID</param>
    /// <param name="language">Language for achievement names (optional)</param>
    /// <returns>User's achievements for the specified game</returns>
    [HttpGet("achievements/{steamId}/{appId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetPlayerAchievements(
        string steamId,
        int appId,
        [FromQuery] string? language = null)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["steamid"] = steamId,
                ["appid"] = appId.ToString()
            };

            if (!string.IsNullOrEmpty(language))
            {
                parameters["l"] = language;
            }

            _logger.LogInformation("Getting achievements for user {SteamId} in game {AppId}", steamId, appId);
            
            var response = await _steamApi.GetAsync("ISteamUserStats", "GetPlayerAchievements", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get achievements for user {SteamId} in game {AppId}", steamId, appId);
            return StatusCode(500, "Failed to retrieve achievements from Steam API");
        }
    }

    /// <summary>
    /// Get user statistics for a specific game
    /// </summary>
    /// <param name="steamId">Steam ID of the user</param>
    /// <param name="appId">Steam App ID</param>
    /// <returns>User's statistics for the specified game</returns>
    [HttpGet("stats/{steamId}/{appId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserStatsForGame(string steamId, int appId)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        try
        {
            _logger.LogInformation("Getting stats for user {SteamId} in game {AppId}", steamId, appId);
            
            var response = await _steamApi.GetAsync("ISteamUserStats", "GetUserStatsForGame", "v0002", 
                new Dictionary<string, string> 
                { 
                    ["steamid"] = steamId,
                    ["appid"] = appId.ToString()
                });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get stats for user {SteamId} in game {AppId}", steamId, appId);
            return StatusCode(500, "Failed to retrieve user statistics from Steam API");
        }
    }
} 