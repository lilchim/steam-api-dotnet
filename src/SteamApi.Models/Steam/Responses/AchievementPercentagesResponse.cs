using SteamApi.Models.Steam.Game;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetGlobalAchievementPercentagesForApp endpoint
/// </summary>
public class AchievementPercentagesResponse
{
    /// <summary>
    /// The application ID
    /// </summary>
    public int AppId { get; set; }
    
    /// <summary>
    /// List of achievement percentages
    /// </summary>
    public List<AchievementPercentage> Achievements { get; set; } = new();
}

/// <summary>
/// Global achievement percentage information
/// </summary>
public class AchievementPercentage
{
    /// <summary>
    /// The API name of the achievement
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The percentage of players who have achieved this
    /// </summary>
    public double Percent { get; set; }
} 