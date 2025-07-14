using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;
using SteamApi.Models.Steam.Responses;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(ISteamApiService steamApi, ILogger<PlayerController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of games a player owns along with playtime information
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <param name="includeAppInfo">Include game name and logo information in the output</param>
    /// <param name="includePlayedFreeGames">Include free games the player has played</param>
    /// <param name="appIdsFilter">Comma-separated list of app IDs to filter the results</param>
    /// <returns>List of owned games with playtime information</returns>
    [HttpGet("owned-games/{steamId}")]
    [ProducesResponseType(typeof(SteamResponse<OwnedGamesResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<OwnedGamesResponse>>> GetOwnedGames(
        string steamId,
        [FromQuery] bool includeAppInfo = false,
        [FromQuery] bool includePlayedFreeGames = false,
        [FromQuery] string? appIdsFilter = null)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["steamid"] = steamId,
                ["include_appinfo"] = includeAppInfo.ToString().ToLower(),
                ["include_played_free_games"] = includePlayedFreeGames.ToString().ToLower()
            };

            if (!string.IsNullOrEmpty(appIdsFilter))
            {
                // Validate app IDs format
                var appIds = appIdsFilter.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var appId in appIds)
                {
                    if (!int.TryParse(appId.Trim(), out _))
                    {
                        return BadRequest($"Invalid App ID format: {appId}");
                    }
                }
                parameters["appids_filter"] = appIdsFilter;
            }

            _logger.LogInformation("Getting owned games for Steam ID {SteamId} with app info: {IncludeAppInfo}", steamId, includeAppInfo);
            
            var response = await _steamApi.GetAsync("IPlayerService", "GetOwnedGames", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get owned games for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve owned games from Steam API");
        }
    }

    /// <summary>
    /// Get a list of games a player has played in the last two weeks
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <param name="count">Optionally limit to a certain number of games</param>
    /// <returns>List of recently played games with playtime information</returns>
    [HttpGet("recent-games/{steamId}")]
    [ProducesResponseType(typeof(SteamResponse<RecentlyPlayedGamesResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<RecentlyPlayedGamesResponse>>> GetRecentlyPlayedGames(
        string steamId,
        [FromQuery] int? count = null)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        if (count.HasValue && (count.Value <= 0 || count.Value > 100))
        {
            return BadRequest("Count must be between 1 and 100");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["steamid"] = steamId
            };

            if (count.HasValue)
            {
                parameters["count"] = count.Value.ToString();
            }

            _logger.LogInformation("Getting recently played games for Steam ID {SteamId} with count: {Count}", steamId, count);
            
            var response = await _steamApi.GetAsync("IPlayerService", "GetRecentlyPlayedGames", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get recently played games for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve recently played games from Steam API");
        }
    }

    /// <summary>
    /// Get the Steam level of a user
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <returns>User's Steam level</returns>
    [HttpGet("level/{steamId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSteamLevel(string steamId)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        try
        {
            _logger.LogInformation("Getting Steam level for Steam ID {SteamId}", steamId);
            
            var response = await _steamApi.GetAsync("IPlayerService", "GetSteamLevel", "v0001", 
                new Dictionary<string, string> { ["steamid"] = steamId });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Steam level for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve Steam level from Steam API");
        }
    }

    /// <summary>
    /// Get badges for a user
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <returns>List of user's badges</returns>
    [HttpGet("badges/{steamId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetBadges(string steamId)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        try
        {
            _logger.LogInformation("Getting badges for Steam ID {SteamId}", steamId);
            
            var response = await _steamApi.GetAsync("IPlayerService", "GetBadges", "v0001", 
                new Dictionary<string, string> { ["steamid"] = steamId });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get badges for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve badges from Steam API");
        }
    }

    /// <summary>
    /// Get community badge progress for a user
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <param name="badgeId">The badge ID to get progress for</param>
    /// <returns>Badge progress information</returns>
    [HttpGet("badge-progress/{steamId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetCommunityBadgeProgress(
        string steamId,
        [FromQuery] int? badgeId = null)
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["steamid"] = steamId
            };

            if (badgeId.HasValue)
            {
                parameters["badgeid"] = badgeId.Value.ToString();
            }

            _logger.LogInformation("Getting badge progress for Steam ID {SteamId} with badge ID: {BadgeId}", steamId, badgeId);
            
            var response = await _steamApi.GetAsync("IPlayerService", "GetCommunityBadgeProgress", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get badge progress for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve badge progress from Steam API");
        }
    }
} 