using SteamApi.Models.Steam.Player;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetOwnedGames endpoint
/// </summary>
public class OwnedGamesResponse
{
    /// <summary>
    /// Total number of games owned
    /// </summary>
    [JsonPropertyName("game_count")]
    public int GameCount { get; set; }
    
    /// <summary>
    /// List of owned games
    /// </summary>
    [JsonPropertyName("games")]
    public List<OwnedGame> Games { get; set; } = new();
} 