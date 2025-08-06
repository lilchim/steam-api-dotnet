using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Ban information for a Steam user
/// </summary>
public class PlayerBans
{
    /// <summary>
    /// 64-bit Steam ID of the user
    /// </summary>
    [JsonPropertyName("SteamId")]
    public string SteamId { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the user is banned from Steam Community
    /// </summary>
    [JsonPropertyName("CommunityBanned")]
    public bool CommunityBanned { get; set; }
    
    /// <summary>
    /// Whether the user has VAC bans
    /// </summary>
    [JsonPropertyName("VACBanned")]
    public bool VacBanned { get; set; }
    
    /// <summary>
    /// Number of VAC bans
    /// </summary>
    [JsonPropertyName("NumberOfVACBans")]
    public int NumberOfVacBans { get; set; }
    
    /// <summary>
    /// Days since last ban
    /// </summary>
    [JsonPropertyName("DaysSinceLastBan")]
    public int DaysSinceLastBan { get; set; }
    
    /// <summary>
    /// Number of game bans
    /// </summary>
    [JsonPropertyName("NumberOfGameBans")]
    public int NumberOfGameBans { get; set; }
    
    /// <summary>
    /// Economy ban status
    /// </summary>
    [JsonPropertyName("EconomyBan")]
    public string EconomyBan { get; set; } = string.Empty;
} 