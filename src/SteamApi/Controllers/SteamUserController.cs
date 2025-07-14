using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;
using SteamApi.Models.Steam.Responses;
using SteamApi.Models.Steam.Player;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamUserController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<SteamUserController> _logger;

    public SteamUserController(ISteamApiService steamApi, ILogger<SteamUserController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get basic profile information for a list of Steam users
    /// </summary>
    /// <param name="steamIds">Comma-delimited list of 64-bit Steam IDs (up to 100)</param>
    /// <returns>Basic profile information for the specified users</returns>
    [HttpGet("summaries")]
    [ProducesResponseType(typeof(SteamResponse<PlayerSummariesResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<PlayerSummariesResponse>>> GetPlayerSummaries([FromQuery] string steamIds)
    {
        if (string.IsNullOrEmpty(steamIds))
        {
            return BadRequest("Steam IDs are required");
        }

        // Validate Steam IDs format and count
        var steamIdArray = steamIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (steamIdArray.Length == 0)
        {
            return BadRequest("At least one Steam ID is required");
        }

        if (steamIdArray.Length > 100)
        {
            return BadRequest("Maximum of 100 Steam IDs allowed per request");
        }

        // Validate each Steam ID is numeric
        foreach (var steamId in steamIdArray)
        {
            if (!ulong.TryParse(steamId.Trim(), out _))
            {
                return BadRequest($"Invalid Steam ID format: {steamId}");
            }
        }

        try
        {
            _logger.LogInformation("Getting player summaries for {Count} Steam IDs", steamIdArray.Length);
            
            var response = await _steamApi.GetAsync("ISteamUser", "GetPlayerSummaries", "v0002", 
                new Dictionary<string, string> { ["steamids"] = steamIds });
            
            // For now, return the raw response since we need to deserialize it properly
            // TODO: Deserialize into strongly-typed models
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get player summaries for Steam IDs: {SteamIds}", steamIds);
            return StatusCode(500, "Failed to retrieve player summaries from Steam API");
        }
    }

    /// <summary>
    /// Get the friend list of a Steam user
    /// </summary>
    /// <param name="steamId">64-bit Steam ID of the user</param>
    /// <param name="relationship">Filter by relationship type (all, friend)</param>
    /// <returns>List of friends for the specified user</returns>
    [HttpGet("friends/{steamId}")]
    [ProducesResponseType(typeof(SteamResponse<FriendListResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<FriendListResponse>>> GetFriendList(
        string steamId,
        [FromQuery] string relationship = "all")
    {
        if (string.IsNullOrEmpty(steamId))
        {
            return BadRequest("Steam ID is required");
        }

        if (!ulong.TryParse(steamId, out _))
        {
            return BadRequest("Invalid Steam ID format");
        }

        if (!string.IsNullOrEmpty(relationship) && relationship != "all" && relationship != "friend")
        {
            return BadRequest("Relationship must be 'all' or 'friend'");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["steamid"] = steamId,
                ["relationship"] = relationship
            };

            _logger.LogInformation("Getting friend list for Steam ID {SteamId} with relationship {Relationship}", steamId, relationship);
            
            var response = await _steamApi.GetAsync("ISteamUser", "GetFriendList", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get friend list for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve friend list from Steam API");
        }
    }

    /// <summary>
    /// Get ban information for a list of Steam users
    /// </summary>
    /// <param name="steamIds">Comma-delimited list of 64-bit Steam IDs</param>
    /// <returns>Ban information for the specified users</returns>
    [HttpGet("bans")]
    [ProducesResponseType(typeof(SteamResponse<PlayerBansResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<PlayerBansResponse>>> GetPlayerBans([FromQuery] string steamIds)
    {
        if (string.IsNullOrEmpty(steamIds))
        {
            return BadRequest("Steam IDs are required");
        }

        // Validate Steam IDs format
        var steamIdArray = steamIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (steamIdArray.Length == 0)
        {
            return BadRequest("At least one Steam ID is required");
        }

        foreach (var steamId in steamIdArray)
        {
            if (!ulong.TryParse(steamId.Trim(), out _))
            {
                return BadRequest($"Invalid Steam ID format: {steamId}");
            }
        }

        try
        {
            _logger.LogInformation("Getting player bans for {Count} Steam IDs", steamIdArray.Length);
            
            var response = await _steamApi.GetAsync("ISteamUser", "GetPlayerBans", "v1", 
                new Dictionary<string, string> { ["steamids"] = steamIds });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get player bans for Steam IDs: {SteamIds}", steamIds);
            return StatusCode(500, "Failed to retrieve player bans from Steam API");
        }
    }

    /// <summary>
    /// Get the groups that a Steam user is a member of
    /// </summary>
    /// <param name="steamId">64-bit Steam ID of the user</param>
    /// <returns>List of groups the user is a member of</returns>
    [HttpGet("groups/{steamId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserGroupList(string steamId)
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
            _logger.LogInformation("Getting group list for Steam ID {SteamId}", steamId);
            
            var response = await _steamApi.GetAsync("ISteamUser", "GetUserGroupList", "v1", 
                new Dictionary<string, string> { ["steamid"] = steamId });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get group list for Steam ID {SteamId}", steamId);
            return StatusCode(500, "Failed to retrieve group list from Steam API");
        }
    }

    /// <summary>
    /// Resolve a vanity URL to a Steam ID
    /// </summary>
    /// <param name="vanityUrl">The vanity URL to resolve</param>
    /// <param name="urlType">The type of vanity URL (1 for individual profile, 2 for group)</param>
    /// <returns>Steam ID information for the vanity URL</returns>
    [HttpGet("resolve/{vanityUrl}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ResolveVanityURL(
        string vanityUrl,
        [FromQuery] int urlType = 1)
    {
        if (string.IsNullOrEmpty(vanityUrl))
        {
            return BadRequest("Vanity URL is required");
        }

        if (urlType != 1 && urlType != 2)
        {
            return BadRequest("URL type must be 1 (individual profile) or 2 (group)");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["vanityurl"] = vanityUrl,
                ["url_type"] = urlType.ToString()
            };

            _logger.LogInformation("Resolving vanity URL {VanityUrl} with type {UrlType}", vanityUrl, urlType);
            
            var response = await _steamApi.GetAsync("ISteamUser", "ResolveVanityURL", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to resolve vanity URL {VanityUrl}", vanityUrl);
            return StatusCode(500, "Failed to resolve vanity URL from Steam API");
        }
    }
} 