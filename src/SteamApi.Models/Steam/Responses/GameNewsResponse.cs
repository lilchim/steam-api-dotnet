using SteamApi.Models.Steam.Game;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetNewsForApp endpoint
/// </summary>
public class GameNewsResponse
{
    /// <summary>
    /// App news wrapper
    /// </summary>
    [JsonPropertyName("appnews")]
    public AppNews AppNews { get; set; } = new();
}

/// <summary>
/// App news information
/// </summary>
public class AppNews
{
    /// <summary>
    /// The application ID
    /// </summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    
    /// <summary>
    /// List of news items
    /// </summary>
    [JsonPropertyName("newsitems")]
    public List<GameNews> NewsItems { get; set; } = new();
    
    /// <summary>
    /// The number of news items returned
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
} 