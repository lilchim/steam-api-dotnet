using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Game;

/// <summary>
/// News article for a Steam game
/// </summary>
public class GameNews
{
    /// <summary>
    /// The unique identifier of the news item
    /// </summary>
    [JsonPropertyName("gid")]
    public string Gid { get; set; } = string.Empty;
    
    /// <summary>
    /// The title of the news item
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// The URL of the news item
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the news item is external
    /// </summary>
    [JsonPropertyName("is_external_url")]
    public bool IsExternalUrl { get; set; }
    
    /// <summary>
    /// The author of the news item
    /// </summary>
    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;
    
    /// <summary>
    /// The contents of the news item
    /// </summary>
    [JsonPropertyName("contents")]
    public string Contents { get; set; } = string.Empty;
    
    /// <summary>
    /// The label of the news item
    /// </summary>
    [JsonPropertyName("feedlabel")]
    public string FeedLabel { get; set; } = string.Empty;
    
    /// <summary>
    /// The date the news item was published
    /// </summary>
    [JsonPropertyName("date")]
    public ulong Date { get; set; }
    
    /// <summary>
    /// The feed name
    /// </summary>
    [JsonPropertyName("feedname")]
    public string FeedName { get; set; } = string.Empty;
    
    /// <summary>
    /// The feed type
    /// </summary>
    [JsonPropertyName("feed_type")]
    public int FeedType { get; set; }
    
    /// <summary>
    /// The app ID of the game
    /// </summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
} 