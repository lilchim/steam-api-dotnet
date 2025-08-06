using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Information about a recently played game
/// </summary>
public class RecentlyPlayedGame
{
    /// <summary>
    /// The application ID of the game
    /// </summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    
    /// <summary>
    /// The name of the game
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The total number of minutes played in the last 2 weeks
    /// </summary>
    [JsonPropertyName("playtime_2weeks")]
    public int Playtime2Weeks { get; set; }
    
    /// <summary>
    /// The total number of minutes played "on record"
    /// </summary>
    [JsonPropertyName("playtime_forever")]
    public int PlaytimeForever { get; set; }
    
    /// <summary>
    /// The icon URL for the game
    /// </summary>
    [JsonPropertyName("img_icon_url")]
    public string ImgIconUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// The logo URL for the game
    /// </summary>
    [JsonPropertyName("img_logo_url")]
    public string ImgLogoUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the game has stats
    /// </summary>
    [JsonPropertyName("has_community_visible_stats")]
    public bool HasCommunityVisibleStats { get; set; }
    
    /// <summary>
    /// Whether the game has leaderboards
    /// </summary>
    [JsonPropertyName("has_leaderboards")]
    public bool HasLeaderboards { get; set; }
    
    /// <summary>
    /// The last time the game was played (Unix timestamp)
    /// </summary>
    [JsonPropertyName("last_played")]
    public ulong LastPlayed { get; set; }
    
    /// <summary>
    /// The total number of minutes played on Windows
    /// </summary>
    [JsonPropertyName("playtime_windows_forever")]
    public int PlaytimeWindowsForever { get; set; }
    
    /// <summary>
    /// The total number of minutes played on Mac
    /// </summary>
    [JsonPropertyName("playtime_mac_forever")]
    public int PlaytimeMacForever { get; set; }
    
    /// <summary>
    /// The total number of minutes played on Linux
    /// </summary>
    [JsonPropertyName("playtime_linux_forever")]
    public int PlaytimeLinuxForever { get; set; }
    
    /// <summary>
    /// The total number of minutes played on Steam Deck
    /// </summary>
    [JsonPropertyName("playtime_deck_forever")]
    public int PlaytimeDeckForever { get; set; }
} 