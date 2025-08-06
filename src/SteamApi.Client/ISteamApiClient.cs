using SteamApi.Models.Steam.Responses;
using SteamApi.Models.Steam.Store;
using SteamApi.Models.Status;

namespace SteamApi.Client;

/// <summary>
/// Client interface for Steam API service
/// </summary>
public interface ISteamApiClient
{
    /// <summary>
    /// Get the current status and configuration of the Steam API service
    /// </summary>
    /// <returns>Status information including configuration details</returns>
    Task<StatusResponse> GetStatusAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get basic profile information for a list of Steam users
    /// </summary>
    /// <param name="steamIds">Comma-delimited list of 64-bit Steam IDs (up to 100)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Basic profile information for the specified users</returns>
    Task<SteamResponse<PlayerSummariesResponse>> GetPlayerSummariesAsync(string steamIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the friend list of a Steam user
    /// </summary>
    /// <param name="steamId">64-bit Steam ID of the user</param>
    /// <param name="relationship">Filter by relationship type (all, friend)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of friends for the specified user</returns>
    Task<SteamResponse<FriendListResponse>> GetFriendListAsync(string steamId, string relationship = "all", CancellationToken cancellationToken = default);

    /// <summary>
    /// Get ban information for a list of Steam users
    /// </summary>
    /// <param name="steamIds">Comma-delimited list of 64-bit Steam IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Ban information for the specified users</returns>
    Task<SteamResponse<PlayerBansResponse>> GetPlayerBansAsync(string steamIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of games a player owns along with playtime information
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <param name="includeAppInfo">Include game name and logo information in the output</param>
    /// <param name="includePlayedFreeGames">Include free games the player has played</param>
    /// <param name="appIdsFilter">Comma-separated list of app IDs to filter the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of owned games with playtime information</returns>
    Task<SteamResponse<OwnedGamesResponse>> GetOwnedGamesAsync(
        string steamId, 
        bool includeAppInfo = false, 
        bool includePlayedFreeGames = false, 
        string? appIdsFilter = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of games a player has played in the last two weeks
    /// </summary>
    /// <param name="steamId">The SteamID of the account</param>
    /// <param name="count">Optionally limit to a certain number of games</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of recently played games with playtime information</returns>
    Task<SteamResponse<RecentlyPlayedGamesResponse>> GetRecentlyPlayedGamesAsync(string steamId, int? count = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get news articles for a specific Steam app
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <param name="count">Number of news items to return (max 20)</param>
    /// <param name="maxLength">Maximum length of each news item (0 for no limit)</param>
    /// <param name="feeds">Comma-separated list of feed names to return news from</param>
    /// <param name="tags">Comma-separated list of tags to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>News articles for the specified app</returns>
    Task<SteamResponse<GameNewsResponse>> GetNewsForAppAsync(
        int appId, 
        int count = 20, 
        int maxLength = 0, 
        string? feeds = null, 
        string? tags = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get global achievement percentages for a specific Steam app
    /// </summary>
    /// <param name="gameId">Steam App ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Global achievement completion percentages</returns>
    Task<SteamResponse<AchievementPercentagesResponse>> GetGlobalAchievementPercentagesForAppAsync(int gameId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get achievements for a specific user and game
    /// </summary>
    /// <param name="steamId">Steam ID of the user</param>
    /// <param name="appId">Steam App ID</param>
    /// <param name="language">Language for achievement names (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User's achievements for the specified game</returns>
    Task<SteamResponse<PlayerAchievementsResponse>> GetPlayerAchievementsAsync(
        string steamId, 
        int appId, 
        string? language = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of all Steam applications
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Complete list of all Steam apps with their IDs and names</returns>
    Task<SteamResponse<AppListResponse>> GetAppListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get detailed information about a Steam app from the store
    /// </summary>
    /// <param name="appId">Steam App ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Detailed app information from the Steam store</returns>
    Task<Dictionary<string, StoreAppDetailsResponse>> GetStoreAppDetailsAsync(int appId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get detailed information about multiple Steam apps from the store
    /// </summary>
    /// <param name="appIds">Comma-separated list of Steam App IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Detailed app information from the Steam store</returns>
    Task<Dictionary<string, StoreAppDetailsResponse>> GetStoreAppDetailsMultipleAsync(string appIds, CancellationToken cancellationToken = default);
} 