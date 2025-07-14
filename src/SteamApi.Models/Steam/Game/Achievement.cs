namespace SteamApi.Models.Steam.Game;

/// <summary>
/// Achievement information for a Steam game
/// </summary>
public class Achievement
{
    /// <summary>
    /// The API name of the achievement
    /// </summary>
    public string ApiName { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the achievement has been achieved
    /// </summary>
    public bool Achieved { get; set; }
    
    /// <summary>
    /// The name of the achievement
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The description of the achievement
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// The icon URL for the achievement (locked)
    /// </summary>
    public string Icon { get; set; } = string.Empty;
    
    /// <summary>
    /// The icon URL for the achievement (unlocked)
    /// </summary>
    public string IconGray { get; set; } = string.Empty;
    
    /// <summary>
    /// The global percentage of players who have achieved this
    /// </summary>
    public double GlobalPercentage { get; set; }
    
    /// <summary>
    /// When the achievement was unlocked (if achieved)
    /// </summary>
    public DateTime? UnlockTime { get; set; }
} 