using SteamApi.Models.Steam.Player;

namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Response for the GetFriendList endpoint
/// </summary>
public class FriendListResponse
{
    /// <summary>
    /// List of friends
    /// </summary>
    public List<Friend> Friends { get; set; } = new();
} 