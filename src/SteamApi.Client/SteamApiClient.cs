using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SteamApi.Client.Configuration;
using SteamApi.Models.Steam.Responses;
using SteamApi.Models.Steam.Store;
using SteamApi.Models.Status;
using System.Text.Json;

namespace SteamApi.Client;

/// <summary>
/// Client implementation for Steam API service
/// </summary>
public class SteamApiClient : ISteamApiClient
{
    private readonly HttpClient _httpClient;
    private readonly SteamApiClientOptions _options;
    private readonly ILogger<SteamApiClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public SteamApiClient(
        HttpClient httpClient, 
        IOptions<SteamApiClientOptions> options, 
        ILogger<SteamApiClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <inheritdoc/>
    public async Task<StatusResponse> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<StatusResponse>("/api/status", cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<PlayerSummariesResponse>> GetPlayerSummariesAsync(string steamIds, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string> { ["steamIds"] = steamIds };
        return await GetAsync<SteamResponse<PlayerSummariesResponse>>("/api/steamuser/summaries", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<FriendListResponse>> GetFriendListAsync(string steamId, string relationship = "all", CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string> { ["relationship"] = relationship };
        return await GetAsync<SteamResponse<FriendListResponse>>($"/api/steamuser/friends/{steamId}", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<PlayerBansResponse>> GetPlayerBansAsync(string steamIds, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string> { ["steamIds"] = steamIds };
        return await GetAsync<SteamResponse<PlayerBansResponse>>("/api/steamuser/bans", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<OwnedGamesResponse>> GetOwnedGamesAsync(
        string steamId, 
        bool includeAppInfo = false, 
        bool includePlayedFreeGames = false, 
        string? appIdsFilter = null, 
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["includeAppInfo"] = includeAppInfo.ToString().ToLower(),
            ["includePlayedFreeGames"] = includePlayedFreeGames.ToString().ToLower()
        };

        if (!string.IsNullOrEmpty(appIdsFilter))
        {
            queryParams["appIdsFilter"] = appIdsFilter;
        }

        return await GetAsync<SteamResponse<OwnedGamesResponse>>($"/api/player/owned-games/{steamId}", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<RecentlyPlayedGamesResponse>> GetRecentlyPlayedGamesAsync(string steamId, int? count = null, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (count.HasValue)
        {
            queryParams["count"] = count.Value.ToString();
        }

        return await GetAsync<SteamResponse<RecentlyPlayedGamesResponse>>($"/api/player/recent-games/{steamId}", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<GameNewsResponse>> GetNewsForAppAsync(
        int appId, 
        int count = 20, 
        int maxLength = 0, 
        string? feeds = null, 
        string? tags = null, 
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["count"] = count.ToString(),
            ["maxLength"] = maxLength.ToString()
        };

        if (!string.IsNullOrEmpty(feeds))
        {
            queryParams["feeds"] = feeds;
        }

        if (!string.IsNullOrEmpty(tags))
        {
            queryParams["tags"] = tags;
        }

        return await GetAsync<SteamResponse<GameNewsResponse>>($"/api/steamnews/app/{appId}", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<AchievementPercentagesResponse>> GetGlobalAchievementPercentagesForAppAsync(int gameId, CancellationToken cancellationToken = default)
    {
        return await GetAsync<SteamResponse<AchievementPercentagesResponse>>($"/api/steamuserstats/achievements/{gameId}/global", cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<PlayerAchievementsResponse>> GetPlayerAchievementsAsync(
        string steamId, 
        int appId, 
        string? language = null, 
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(language))
        {
            queryParams["language"] = language;
        }

        return await GetAsync<SteamResponse<PlayerAchievementsResponse>>($"/api/steamuserstats/achievements/{steamId}/{appId}", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<AppListResponse>> GetAppListAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<SteamResponse<AppListResponse>>("/api/steamapps/list", cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<string, StoreAppDetailsResponse>> GetStoreAppDetailsAsync(int appId, CancellationToken cancellationToken = default)
    {
        return await GetAsync<Dictionary<string, StoreAppDetailsResponse>>($"/api/steamstore/appdetails/{appId}", cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<string, StoreAppDetailsResponse>> GetStoreAppDetailsMultipleAsync(string appIds, CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string> { ["appIds"] = appIds };
        return await GetAsync<Dictionary<string, StoreAppDetailsResponse>>("/api/steamstore/appdetails", queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SteamResponse<VanityUrlResponse>> ResolveVanityUrlAsync(string vanityUrl, int urlType = 1, CancellationToken cancellationToken = default)
    {
        // Extract the vanity URL from a full Steam Community URL if provided
        var extractedVanityUrl = ExtractVanityUrlFromUrl(vanityUrl);
        
        var queryParams = new Dictionary<string, string>
        {
            ["urlType"] = urlType.ToString()
        };

        return await GetAsync<SteamResponse<VanityUrlResponse>>($"/api/steamuser/resolve/{extractedVanityUrl}", queryParams, cancellationToken);
    }

    /// <summary>
    /// Extracts the vanity URL from a full Steam Community URL or returns the input if it's already a vanity URL
    /// </summary>
    /// <param name="input">The input which could be a full URL or just a vanity URL</param>
    /// <returns>The extracted vanity URL</returns>
    private static string ExtractVanityUrlFromUrl(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Vanity URL cannot be null or empty", nameof(input));
        }

        // If it's already just a vanity URL (no slashes or dots), return it as-is
        if (!input.Contains('/') && !input.Contains('.'))
        {
            return input;
        }

        // Try to parse as a Steam Community URL
        if (input.Contains("steamcommunity.com/id/"))
        {
            var parts = input.Split(new[] { "steamcommunity.com/id/" }, StringSplitOptions.None);
            if (parts.Length > 1)
            {
                var vanityUrl = parts[1].Split('/')[0]; 
                return vanityUrl;
            }
        }

        return input;
    }

    private async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        return await GetAsync<T>(endpoint, null, cancellationToken);
    }

    private async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams, CancellationToken cancellationToken = default)
    {
        var url = BuildUrl(endpoint, queryParams);
        
        if (_options.EnableLogging)
        {
            _logger.LogInformation("Making GET request to {Url}", url);
        }

        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);
            
            if (_options.EnableLogging)
            {
                _logger.LogInformation("Received response with status {StatusCode} from {Url}", response.StatusCode, url);
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

            if (result == null)
            {
                throw new InvalidOperationException($"Failed to deserialize response from {url}");
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for {Url}", url);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to deserialize response from {Url}", url);
            throw;
        }
    }

    private string BuildUrl(string endpoint, Dictionary<string, string>? queryParams)
    {
        var baseUrl = _options.BaseUrl.TrimEnd('/');
        var url = $"{baseUrl}{endpoint}";

        if (queryParams?.Any() == true)
        {
            var queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            url += $"?{queryString}";
        }

        return url;
    }
} 