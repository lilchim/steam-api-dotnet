using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Game;

/// <summary>
/// Achievement information for a Steam game
/// </summary>
public class Achievement
{
    /// <summary>
    /// The API name of the achievement
    /// </summary>
    [JsonPropertyName("apiname")]
    public string ApiName { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the achievement has been achieved (1 for achieved, 0 for not achieved)
    /// </summary>
    [JsonPropertyName("achieved")]
    public int Achieved { get; set; }
    
    /// <summary>
    /// When the achievement was unlocked (if achieved) (Unix timestamp)
    /// </summary>
    [JsonPropertyName("unlocktime")]
    public ulong UnlockTime { get; set; }
} 