using SteamApi.Models.Steam.Game;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetPlayerAchievements endpoint
/// </summary>
public class PlayerAchievementsResponse
{
    /// <summary>
    /// Player statistics wrapper
    /// </summary>
    [JsonPropertyName("playerstats")]
    public PlayerStats PlayerStats { get; set; } = new();
}

/// <summary>
/// Player statistics information
/// </summary>
public class PlayerStats
{
    /// <summary>
    /// The Steam ID of the player
    /// </summary>
    [JsonPropertyName("steamID")]
    public string SteamId { get; set; } = string.Empty;
    
    /// <summary>
    /// The name of the game
    /// </summary>
    [JsonPropertyName("gameName")]
    public string GameName { get; set; } = string.Empty;
    
    /// <summary>
    /// List of achievements
    /// </summary>
    [JsonPropertyName("achievements")]
    public List<Achievement> Achievements { get; set; } = new();
    
    /// <summary>
    /// Whether the request was successful
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
} 