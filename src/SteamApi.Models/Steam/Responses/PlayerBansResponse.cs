using SteamApi.Models.Steam.Player;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetPlayerBans endpoint
/// </summary>
public class PlayerBansResponse
{
    /// <summary>
    /// List of player bans
    /// </summary>
    public List<PlayerBans> Players { get; set; } = new();
} 