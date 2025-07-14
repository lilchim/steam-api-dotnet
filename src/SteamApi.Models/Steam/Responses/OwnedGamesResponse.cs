using SteamApi.Models.Steam.Player;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetOwnedGames endpoint
/// </summary>
public class OwnedGamesResponse
{
    /// <summary>
    /// Total number of games owned
    /// </summary>
    public int GameCount { get; set; }
    
    /// <summary>
    /// List of owned games
    /// </summary>
    public List<OwnedGame> Games { get; set; } = new();
} 