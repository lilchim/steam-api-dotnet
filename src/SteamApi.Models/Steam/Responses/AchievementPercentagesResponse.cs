using SteamApi.Models.Steam.Game;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetGlobalAchievementPercentagesForApp endpoint
/// </summary>
public class AchievementPercentagesResponse
{
    /// <summary>
    /// The application ID
    /// </summary>
    [JsonPropertyName("appid")]
    public int AppId { get; set; }
    
    /// <summary>
    /// List of achievement percentages
    /// </summary>
    [JsonPropertyName("achievements")]
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
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The percentage of players who have achieved this
    /// </summary>
    [JsonPropertyName("percent")]
    public double Percent { get; set; }
} 