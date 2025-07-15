using SteamApi.Models.Steam.Player;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetPlayerSummaries endpoint
/// </summary>
public class PlayerSummariesResponse
{
    /// <summary>
    /// List of player summaries
    /// </summary>
    [JsonPropertyName("players")]
    public List<PlayerSummary> Players { get; set; } = new();
} 