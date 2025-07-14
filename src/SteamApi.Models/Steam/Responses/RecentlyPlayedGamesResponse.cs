using SteamApi.Models.Steam.Player;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetRecentlyPlayedGames endpoint
/// </summary>
public class RecentlyPlayedGamesResponse
{
    /// <summary>
    /// Total number of recently played games
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// List of recently played games
    /// </summary>
    public List<RecentlyPlayedGame> Games { get; set; } = new();
} 