using SteamApi.Models.Steam.Player;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetFriendList endpoint
/// </summary>
public class FriendListResponse
{
    /// <summary>
    /// List of friends
    /// </summary>
    [JsonPropertyName("friends")]
    public List<Friend> Friends { get; set; } = new();
} 