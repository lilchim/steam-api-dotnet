using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Player;

/// <summary>
/// Basic profile information for a Steam user
/// </summary>
public class PlayerSummary
{
    /// <summary>
    /// 64-bit Steam ID of the user
    /// </summary>
    [JsonPropertyName("steamid")]
    public ulong SteamId { get; set; }
    
    /// <summary>
    /// The player's persona name (display name)
    /// </summary>
    [JsonPropertyName("personaname")]
    public string PersonaName { get; set; } = string.Empty;
    
    /// <summary>
    /// The full URL of the player's Steam Community profile page
    /// </summary>
    [JsonPropertyName("profileurl")]
    public string ProfileUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// The full URL of the player's 32x32 pixel avatar
    /// </summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; } = string.Empty;
    
    /// <summary>
    /// The full URL of the player's 64x64 pixel avatar
    /// </summary>
    [JsonPropertyName("avatarmedium")]
    public string AvatarMedium { get; set; } = string.Empty;
    
    /// <summary>
    /// The full URL of the player's 184x184 pixel avatar
    /// </summary>
    [JsonPropertyName("avatarfull")]
    public string AvatarFull { get; set; } = string.Empty;
    
    /// <summary>
    /// The user's current status
    /// </summary>
    [JsonPropertyName("personastate")]
    public PlayerStatus Status { get; set; }
    
    /// <summary>
    /// The player's "Real Name" if they have set it
    /// </summary>
    [JsonPropertyName("realname")]
    public string? RealName { get; set; }
    
    /// <summary>
    /// The player's primary group, as configured in their Steam Community profile
    /// </summary>
    [JsonPropertyName("primaryclanid")]
    public string? PrimaryClanId { get; set; }
    
    /// <summary>
    /// The time the player was last online
    /// </summary>
    [JsonPropertyName("lastlogoff")]
    public DateTime? LastLogoff { get; set; }
    
    /// <summary>
    /// If set, indicates the profile is currently in the requested visibility state
    /// </summary>
    [JsonPropertyName("profilestate")]
    public int ProfileState { get; set; }
    
    /// <summary>
    /// If set, indicates the profile allows public comments
    /// </summary>
    [JsonPropertyName("commentpermission")]
    public int CommentPermission { get; set; }
    
    /// <summary>
    /// The player's current game ID if they are playing a game
    /// </summary>
    [JsonPropertyName("gameid")]
    public int? GameId { get; set; }
    
    /// <summary>
    /// The title of the game the player is currently playing
    /// </summary>
    [JsonPropertyName("gameextrainfo")]
    public string? GameExtraInfo { get; set; }
    
    /// <summary>
    /// The server URL the player is currently connected to
    /// </summary>
    [JsonPropertyName("gameserverip")]
    public string? GameServerIp { get; set; }
    
    /// <summary>
    /// The time the player's account was created
    /// </summary>
    [JsonPropertyName("timecreated")]
    public DateTime? TimeCreated { get; set; }
    
    /// <summary>
    /// The player's Steam Community ban status
    /// </summary>
    [JsonPropertyName("loccountrycode")]
    public string? LocCountryCode { get; set; }
    
    /// <summary>
    /// The player's state/province
    /// </summary>
    [JsonPropertyName("locstatecode")]
    public string? LocStateCode { get; set; }
    
    /// <summary>
    /// The player's city
    /// </summary>
    [JsonPropertyName("loccityid")]
    public int? LocCityId { get; set; }
}

/// <summary>
/// Steam user status enumeration
/// </summary>
public enum PlayerStatus
{
    /// <summary>
    /// User is offline
    /// </summary>
    Offline = 0,
    
    /// <summary>
    /// User is online
    /// </summary>
    Online = 1,
    
    /// <summary>
    /// User is busy
    /// </summary>
    Busy = 2,
    
    /// <summary>
    /// User is away
    /// </summary>
    Away = 3,
    
    /// <summary>
    /// User is snoozing
    /// </summary>
    Snooze = 4,
    
    /// <summary>
    /// User is looking to trade
    /// </summary>
    LookingToTrade = 5,
    
    /// <summary>
    /// User is looking to play
    /// </summary>
    LookingToPlay = 6
} 