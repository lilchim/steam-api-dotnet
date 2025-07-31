namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Information about a recently played game
/// </summary>
public class RecentlyPlayedGame
{
    /// <summary>
    /// The application ID of the game
    /// </summary>
    public int AppId { get; set; }
    
    /// <summary>
    /// The name of the game
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The total number of minutes played in the last 2 weeks
    /// </summary>
    public int Playtime2Weeks { get; set; }
    
    /// <summary>
    /// The total number of minutes played "on record"
    /// </summary>
    public int PlaytimeForever { get; set; }
    
    /// <summary>
    /// The icon URL for the game
    /// </summary>
    public string ImgIconUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// The logo URL for the game
    /// </summary>
    public string ImgLogoUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the game has stats
    /// </summary>
    public bool HasCommunityVisibleStats { get; set; }
    
    /// <summary>
    /// Whether the game has leaderboards
    /// </summary>
    public bool HasLeaderboards { get; set; }
    
    /// <summary>
    /// The last time the game was played (Unix timestamp)
    /// </summary>
    public ulong LastPlayed { get; set; }
} 