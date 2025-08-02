using Microsoft.AspNetCore.Mvc;
using SteamApi.Services;
using SteamApi.Models.Steam.Store;

namespace SteamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SteamStoreController : ControllerBase
{
    private readonly ISteamApiService _steamApi;
    private readonly ILogger<SteamStoreController> _logger;

    public SteamStoreController(ISteamApiService steamApi, ILogger<SteamStoreController> logger)
    {
        _steamApi = steamApi;
        _logger = logger;
    }

    /// <summary>
    /// Get detailed information about a Steam app from the store
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <returns>Detailed app information from the Steam store</returns>
    [HttpGet("appdetails/{appId}")]
    [ProducesResponseType(typeof(Dictionary<string, StoreAppDetailsResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Dictionary<string, StoreAppDetailsResponse>>> GetAppDetails(int appId)
    {
        if (appId <= 0)
        {
            return BadRequest("App ID must be a positive integer");
        }

        try
        {
            _logger.LogInformation("Getting store details for app {AppId}", appId);
            
            var parameters = new Dictionary<string, string>
            {
                ["appids"] = appId.ToString()
            };
            
            var response = await _steamApi.GetStoreAsync<Dictionary<string, StoreAppDetailsResponse>>("appdetails", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get store details for app {AppId}", appId);
            return StatusCode(500, "Failed to retrieve app details from Steam Store API");
        }
    }

    /// <summary>
    /// Get detailed information about multiple Steam apps from the store
    /// </summary>
    /// <param name="appIds">Comma-separated list of Steam App IDs</param>
    /// <returns>Detailed app information from the Steam store</returns>
    [HttpGet("appdetails")]
    [ProducesResponseType(typeof(Dictionary<string, StoreAppDetailsResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Dictionary<string, StoreAppDetailsResponse>>> GetAppDetailsMultiple([FromQuery] string appIds)
    {
        if (string.IsNullOrWhiteSpace(appIds))
        {
            return BadRequest("App IDs parameter is required");
        }

        // Validate that all app IDs are positive integers
        var appIdList = appIds.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var appIdStr in appIdList)
        {
            if (!int.TryParse(appIdStr.Trim(), out int appId) || appId <= 0)
            {
                return BadRequest($"Invalid app ID: {appIdStr}");
            }
        }

        try
        {
            _logger.LogInformation("Getting store details for apps: {AppIds}", appIds);
            
            var parameters = new Dictionary<string, string>
            {
                ["appids"] = appIds
            };
            
            var response = await _steamApi.GetStoreAsync<Dictionary<string, StoreAppDetailsResponse>>("appdetails", parameters);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get store details for apps: {AppIds}", appIds);
            return StatusCode(500, "Failed to retrieve app details from Steam Store API");
        }
    }
} 