namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Friend information from a Steam user's friend list
/// </summary>
public class Friend
{
    /// <summary>
    /// 64-bit Steam ID of the friend
    /// </summary>
    public ulong SteamId { get; set; }
    
    /// <summary>
    /// The relationship between the user and this friend
    /// </summary>
    public string Relationship { get; set; } = string.Empty;
    
    /// <summary>
    /// When the friend was added to the user's friend list
    /// </summary>
    public DateTime FriendSince { get; set; }
} 