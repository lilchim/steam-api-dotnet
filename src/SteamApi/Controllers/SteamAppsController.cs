using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;
using SteamApi.Models.Steam.Responses;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamAppsController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<SteamAppsController> _logger;

    public SteamAppsController(ISteamApiService steamApi, ILogger<SteamAppsController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get a list of all Steam applications
    /// </summary>
    /// <returns>Complete list of all Steam apps with their IDs and names</returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(SteamResponse<AppListResponse>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SteamResponse<AppListResponse>>> GetAppList()
    {
        try
        {
            _logger.LogInformation("Getting complete Steam app list");
            
            var response = await _steamApi.GetAsync("ISteamApps", "GetAppList", "v0002");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Steam app list");
            return StatusCode(500, "Failed to retrieve Steam app list from Steam API");
        }
    }

    /// <summary>
    /// Get servers at a specific IP address
    /// </summary>
    /// <param name="addr">IP address to query</param>
    /// <returns>List of servers at the specified address</returns>
    [HttpGet("servers/{addr}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetServersAtAddress(string addr)
    {
        if (string.IsNullOrEmpty(addr))
        {
            return BadRequest("IP address is required");
        }

        // Basic IP address validation
        if (!IsValidIpAddress(addr))
        {
            return BadRequest("Invalid IP address format");
        }

        try
        {
            _logger.LogInformation("Getting servers at address {Address}", addr);
            
            var response = await _steamApi.GetAsync("ISteamApps", "GetServersAtAddress", "v0001", 
                new Dictionary<string, string> { ["addr"] = addr });
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get servers at address {Address}", addr);
            return StatusCode(500, "Failed to retrieve server information from Steam API");
        }
    }

    /// <summary>
    /// Check if a Steam app is up to date
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <param name="version">Current version of the app</param>
    /// <returns>Update status information for the app</returns>
    [HttpGet("up-to-date/{appId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpToDateCheck(
        int appId,
        [FromQuery] int version)
    {
        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        if (version <= 0)
        {
            return BadRequest("Version must be a positive integer");
        }

        try
        {
            var parameters = new Dictionary<string, string>
            {
                ["appid"] = appId.ToString(),
                ["version"] = version.ToString()
            };

            _logger.LogInformation("Checking if app {AppId} is up to date with version {Version}", appId, version);
            
            var response = await _steamApi.GetAsync("ISteamApps", "UpToDateCheck", "v0001", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check if app {AppId} is up to date", appId);
            return StatusCode(500, "Failed to retrieve update status from Steam API");
        }
    }

    /// <summary>
    /// Get a list of all Steam applications (alternative endpoint)
    /// </summary>
    /// <returns>Complete list of all Steam apps</returns>
    [HttpGet("list/v2")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAppListV2()
    {
        try
        {
            _logger.LogInformation("Getting complete Steam app list (v2)");
            
            var response = await _steamApi.GetAsync("ISteamApps", "GetAppList", "v2");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Steam app list (v2)");
            return StatusCode(500, "Failed to retrieve Steam app list from Steam API");
        }
    }

    /// <summary>
    /// Get a list of all Steam applications (alternative endpoint)
    /// </summary>
    /// <returns>Complete list of all Steam apps</returns>
    [HttpGet("list/v1")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAppListV1()
    {
        try
        {
            _logger.LogInformation("Getting complete Steam app list (v1)");
            
            var response = await _steamApi.GetAsync("ISteamApps", "GetAppList", "v1");
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get Steam app list (v1)");
            return StatusCode(500, "Failed to retrieve Steam app list from Steam API");
        }
    }

    private static bool IsValidIpAddress(string ipAddress)
    {
        // Basic IP address validation
        if (string.IsNullOrWhiteSpace(ipAddress))
            return false;

        // Check for IPv4 format (x.x.x.x)
        var parts = ipAddress.Split('.');
        if (parts.Length == 4)
        {
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                    return false;
            }
            return true;
        }

        // Check for IPv6 format (simplified check)
        if (ipAddress.Contains(':'))
        {
            // Basic IPv6 validation - just check if it contains colons
            return ipAddress.Split(':').Length <= 8;
        }

        return false;
    }
} 