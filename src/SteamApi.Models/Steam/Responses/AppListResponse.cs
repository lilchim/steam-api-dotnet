using SteamApi.Models.Steam.Game;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetAppList endpoint
/// </summary>
public class AppListResponse
{
    /// <summary>
    /// List of Steam applications
    /// </summary>
    public List<SteamApp> Apps { get; set; } = new();
} 