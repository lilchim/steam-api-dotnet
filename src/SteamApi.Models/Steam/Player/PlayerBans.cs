namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Ban information for a Steam user
/// </summary>
public class PlayerBans
{
    /// <summary>
    /// 64-bit Steam ID of the user
    /// </summary>
    public string SteamId { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the user is banned from Steam Community
    /// </summary>
    public bool CommunityBanned { get; set; }
    
    /// <summary>
    /// Whether the user has VAC bans
    /// </summary>
    public bool VacBanned { get; set; }
    
    /// <summary>
    /// Number of VAC bans
    /// </summary>
    public int NumberOfVacBans { get; set; }
    
    /// <summary>
    /// Days since last ban
    /// </summary>
    public int DaysSinceLastBan { get; set; }
    
    /// <summary>
    /// Number of game bans
    /// </summary>
    public int NumberOfGameBans { get; set; }
    
    /// <summary>
    /// Economy ban status
    /// </summary>
    public string EconomyBan { get; set; } = string.Empty;
} 