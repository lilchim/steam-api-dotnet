namespace SteamApi.Models.Steam.Game;

/// <summary>
/// News article for a Steam game
/// </summary>
public class GameNews
{
    /// <summary>
    /// The unique identifier of the news item
    /// </summary>
    public string Gid { get; set; } = string.Empty;
    
    /// <summary>
    /// The title of the news item
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// The URL of the news item
    /// </summary>
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the news item is external
    /// </summary>
    public bool IsExternalUrl { get; set; }
    
    /// <summary>
    /// The author of the news item
    /// </summary>
    public string Author { get; set; } = string.Empty;
    
    /// <summary>
    /// The contents of the news item
    /// </summary>
    public string Contents { get; set; } = string.Empty;
    
    /// <summary>
    /// The label of the news item
    /// </summary>
    public string FeedLabel { get; set; } = string.Empty;
    
    /// <summary>
    /// The date the news item was published
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// The feed name
    /// </summary>
    public string FeedName { get; set; } = string.Empty;
    
    /// <summary>
    /// The feed type
    /// </summary>
    public int FeedType { get; set; }
    
    /// <summary>
    /// The app ID of the game
    /// </summary>
    public int AppId { get; set; }
} 