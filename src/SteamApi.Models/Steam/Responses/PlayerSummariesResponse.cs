using SteamApi.Models.Steam.Player;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetPlayerSummaries endpoint
/// </summary>
public class PlayerSummariesResponse
{
    /// <summary>
    /// List of player summaries
    /// </summary>
    public List<PlayerSummary> Players { get; set; } = new();
} 