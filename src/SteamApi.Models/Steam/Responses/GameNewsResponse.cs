using SteamApi.Models.Steam.Game;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetNewsForApp endpoint
/// </summary>
public class GameNewsResponse
{
    /// <summary>
    /// The application ID
    /// </summary>
    public int AppId { get; set; }
    
    /// <summary>
    /// List of news items
    /// </summary>
    public List<GameNews> NewsItems { get; set; } = new();
    
    /// <summary>
    /// The number of news items returned
    /// </summary>
    public int Count { get; set; }
} 