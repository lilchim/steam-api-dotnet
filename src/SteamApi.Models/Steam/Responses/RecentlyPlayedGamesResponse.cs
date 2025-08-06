using SteamApi.Models.Steam.Player;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetRecentlyPlayedGames endpoint
/// </summary>
public class RecentlyPlayedGamesResponse
{
    /// <summary>
    /// Total number of recently played games
    /// </summary>
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    
    /// <summary>
    /// List of recently played games
    /// </summary>
    [JsonPropertyName("games")]
    public List<RecentlyPlayedGame> Games { get; set; } = new();
} 