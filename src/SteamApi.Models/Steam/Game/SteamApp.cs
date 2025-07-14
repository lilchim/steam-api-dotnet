namespace SteamApi.Models.Steam.Game;

/// <summary>
/// Basic information about a Steam application
/// </summary>
public class SteamApp
{
    /// <summary>
    /// The application ID
    /// </summary>
    public int AppId { get; set; }
    
    /// <summary>
    /// The name of the application
    /// </summary>
    public string Name { get; set; } = string.Empty;
} 