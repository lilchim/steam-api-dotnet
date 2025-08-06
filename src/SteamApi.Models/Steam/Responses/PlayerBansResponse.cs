using SteamApi.Models.Steam.Player;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetPlayerBans endpoint
/// </summary>
public class PlayerBansResponse
{
    /// <summary>
    /// List of player bans
    /// </summary>
    [JsonPropertyName("players")]
    public List<PlayerBans> Players { get; set; } = new();
} 