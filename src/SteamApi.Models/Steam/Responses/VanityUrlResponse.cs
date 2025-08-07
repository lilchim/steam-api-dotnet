using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the ResolveVanityURL endpoint
/// </summary>
public class VanityUrlResponse
{
    /// <summary>
    /// The Steam ID of the resolved vanity URL
    /// </summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }
    
    /// <summary>
    /// Whether the vanity URL was successfully resolved
    /// </summary>
    [JsonPropertyName("success")]
    public int Success { get; set; }
    
    /// <summary>
    /// Message indicating the result of the resolution
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
} 