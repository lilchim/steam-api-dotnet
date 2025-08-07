using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using SteamApi.Client;
using SteamApi.Client.Configuration;
using SteamApi.Client.Extensions;
using SteamApi.Models.Steam.Responses;
using SteamApi.Models.Steam.Store;
using SteamApi.Models.Status;
using System.Net;
using System.Text.Json;

namespace SteamApi.Client.Tests;

public class SteamApiClientTests
{
    private readonly ServiceCollection _services;
    private readonly Mock<HttpMessageHandler> _mockHttpHandler;

    public SteamApiClientTests()
    {
        _services = new ServiceCollection();
        _mockHttpHandler = new Mock<HttpMessageHandler>();
    }

    [Fact]
    public async Task GetStatusAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new StatusResponse
        {
            Status = "OK",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0"
        };

        SetupMockResponse("/api/status", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetStatusAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Status, result.Status);
        Assert.Equal(expectedResponse.Version, result.Version);
    }

    [Fact]
    public async Task GetPlayerSummariesAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<PlayerSummariesResponse>
        {
            Response = new PlayerSummariesResponse
            {
                Players = new List<SteamApi.Models.Steam.Player.PlayerSummary>
                {
                    new()
                    {
                        SteamId = "76561198000000000",
                        PersonaName = "TestUser",
                        ProfileUrl = "https://steamcommunity.com/id/testuser",
                        Avatar = "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/test.jpg"
                    }
                }
            }
        };

        SetupMockResponse("/api/steamuser/summaries?steamIds=76561198000000000", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetPlayerSummariesAsync("76561198000000000");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Players);
        Assert.Equal("76561198000000000", result.Response.Players[0].SteamId);
        Assert.Equal("TestUser", result.Response.Players[0].PersonaName);
    }

    [Fact]
    public async Task GetPlayerBansAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<PlayerBansResponse>
        {
            Response = new PlayerBansResponse
            {
                Players = new List<SteamApi.Models.Steam.Player.PlayerBans>
                {
                    new()
                    {
                        SteamId = "76561198000000000",
                        CommunityBanned = false,
                        VacBanned = false,
                        NumberOfVacBans = 0,
                        DaysSinceLastBan = 0,
                        NumberOfGameBans = 0,
                        EconomyBan = "none"
                    }
                }
            }
        };

        SetupMockResponse("/api/steamuser/bans?steamIds=76561198000000000", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetPlayerBansAsync("76561198000000000");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Players);
        Assert.Equal("76561198000000000", result.Response.Players[0].SteamId);
        Assert.False(result.Response.Players[0].CommunityBanned);
        Assert.False(result.Response.Players[0].VacBanned);
    }

    [Fact]
    public async Task GetFriendListAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<FriendListResponse>
        {
            Response = new FriendListResponse
            {
                Friends = new List<SteamApi.Models.Steam.Player.Friend>
                {
                    new()
                    {
                        SteamId = "76561198000000001",
                        Relationship = "friend",
                        FriendSince = 1234567890
                    }
                }
            }
        };

        SetupMockResponse("/api/steamuser/friends/76561198000000000?relationship=all", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetFriendListAsync("76561198000000000");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Friends);
        Assert.Equal("76561198000000001", result.Response.Friends[0].SteamId);
        Assert.Equal("friend", result.Response.Friends[0].Relationship);
    }

    [Fact]
    public async Task GetOwnedGamesAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<OwnedGamesResponse>
        {
            Response = new OwnedGamesResponse
            {
                GameCount = 1,
                Games = new List<SteamApi.Models.Steam.Player.OwnedGame>
                {
                    new()
                    {
                        AppId = 730,
                        Name = "Counter-Strike 2",
                        PlaytimeForever = 1000,
                        Playtime2Weeks = 50
                    }
                }
            }
        };

        SetupMockResponse("/api/player/owned-games/76561198000000000?includeAppInfo=false&includePlayedFreeGames=false", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetOwnedGamesAsync("76561198000000000");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal(1, result.Response.GameCount);
        Assert.Single(result.Response.Games);
        Assert.Equal(730, result.Response.Games[0].AppId);
        Assert.Equal("Counter-Strike 2", result.Response.Games[0].Name);
    }

    [Fact]
    public async Task GetRecentlyPlayedGamesAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<RecentlyPlayedGamesResponse>
        {
            Response = new RecentlyPlayedGamesResponse
            {
                TotalCount = 1,
                Games = new List<SteamApi.Models.Steam.Player.RecentlyPlayedGame>
                {
                    new()
                    {
                        AppId = 730,
                        Name = "Counter-Strike 2",
                        Playtime2Weeks = 50,
                        PlaytimeForever = 1000,
                        ImgIconUrl = "test.jpg"
                    }
                }
            }
        };

        SetupMockResponse("/api/player/recent-games/76561198000000000", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetRecentlyPlayedGamesAsync("76561198000000000");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal(1, result.Response.TotalCount);
        Assert.Single(result.Response.Games);
        Assert.Equal(730, result.Response.Games[0].AppId);
        Assert.Equal("Counter-Strike 2", result.Response.Games[0].Name);
    }

    [Fact]
    public async Task GetNewsForAppAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<GameNewsResponse>
        {
            Response = new GameNewsResponse
            {
                AppNews = new AppNews
                {
                    AppId = 730,
                    Count = 1,
                    NewsItems = new List<SteamApi.Models.Steam.Game.GameNews>
                    {
                        new()
                        {
                            Gid = "test-gid",
                            Title = "Test News",
                            Url = "https://example.com",
                            Author = "Test Author",
                            Contents = "Test content",
                            FeedLabel = "Test Feed",
                            Date = 1234567890,
                            FeedName = "test_feed",
                            FeedType = 1,
                            AppId = 730,
                            IsExternalUrl = false
                        }
                    }
                }
            }
        };

        SetupMockResponse("/api/steamnews/app/730?count=20&maxLength=0", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetNewsForAppAsync(730);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.NotNull(result.Response.AppNews);
        Assert.Equal(730, result.Response.AppNews.AppId);
        Assert.Single(result.Response.AppNews.NewsItems);
        Assert.Equal("Test News", result.Response.AppNews.NewsItems[0].Title);
    }

    [Fact]
    public async Task GetPlayerAchievementsAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<PlayerAchievementsResponse>
        {
            Response = new PlayerAchievementsResponse
            {
                PlayerStats = new PlayerStats
                {
                    SteamId = "76561198000000000",
                    GameName = "Test Game",
                    Success = true,
                    Achievements = new List<SteamApi.Models.Steam.Game.Achievement>
                    {
                        new()
                        {
                            ApiName = "test_achievement",
                            Achieved = 1,
                            UnlockTime = 1234567890
                        }
                    }
                }
            }
        };

        SetupMockResponse("/api/steamuserstats/achievements/76561198000000000/730", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetPlayerAchievementsAsync("76561198000000000", 730);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.NotNull(result.Response.PlayerStats);
        Assert.Equal("76561198000000000", result.Response.PlayerStats.SteamId);
        Assert.Equal("Test Game", result.Response.PlayerStats.GameName);
        Assert.True(result.Response.PlayerStats.Success);
        Assert.Single(result.Response.PlayerStats.Achievements);
        Assert.Equal("test_achievement", result.Response.PlayerStats.Achievements[0].ApiName);
        Assert.Equal(1, result.Response.PlayerStats.Achievements[0].Achieved);
    }

    [Fact]
    public async Task GetGlobalAchievementPercentagesForAppAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<AchievementPercentagesResponse>
        {
            Response = new AchievementPercentagesResponse
            {
                AppId = 730,
                Achievements = new List<AchievementPercentage>
                {
                    new()
                    {
                        Name = "test_achievement",
                        Percent = 50.5
                    }
                }
            }
        };

        SetupMockResponse("/api/steamuserstats/achievements/730/global", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetGlobalAchievementPercentagesForAppAsync(730);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal(730, result.Response.AppId);
        Assert.Single(result.Response.Achievements);
        Assert.Equal("test_achievement", result.Response.Achievements[0].Name);
        Assert.Equal(50.5, result.Response.Achievements[0].Percent);
    }

    [Fact]
    public async Task GetAppListAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<AppListResponse>
        {
            Response = new AppListResponse
            {
                Apps = new List<SteamApi.Models.Steam.Game.SteamApp>
                {
                    new()
                    {
                        AppId = 730,
                        Name = "Counter-Strike 2"
                    }
                }
            }
        };

        SetupMockResponse("/api/steamapps/list", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetAppListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Apps);
        Assert.Equal(730, result.Response.Apps[0].AppId);
        Assert.Equal("Counter-Strike 2", result.Response.Apps[0].Name);
    }

    [Fact]
    public async Task GetStoreAppDetailsAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new Dictionary<string, StoreAppDetailsResponse>
        {
            ["730"] = new StoreAppDetailsResponse
            {
                Success = true,
                Data = new StoreAppDetails
                {
                    Name = "Counter-Strike 2",
                    Type = "game",
                    IsFree = false
                }
            }
        };

        SetupMockResponse("/api/steamstore/appdetails/730", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetStoreAppDetailsAsync(730);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.True(result.ContainsKey("730"));
        Assert.True(result["730"].Success);
        Assert.Equal("Counter-Strike 2", result["730"].Data?.Name);
    }

    [Fact]
    public async Task GetStoreAppDetailsMultipleAsync_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new Dictionary<string, StoreAppDetailsResponse>
        {
            ["730"] = new StoreAppDetailsResponse
            {
                Success = true,
                Data = new StoreAppDetails
                {
                    Name = "Counter-Strike 2",
                    Type = "game",
                    IsFree = false
                }
            },
            ["440"] = new StoreAppDetailsResponse
            {
                Success = true,
                Data = new StoreAppDetails
                {
                    Name = "Team Fortress 2",
                    Type = "game",
                    IsFree = true
                }
            }
        };

        SetupMockResponse("/api/steamstore/appdetails?appIds=730,440", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.GetStoreAppDetailsMultipleAsync("730,440");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.True(result.ContainsKey("730"));
        Assert.True(result.ContainsKey("440"));
        Assert.Equal("Counter-Strike 2", result["730"].Data?.Name);
        Assert.Equal("Team Fortress 2", result["440"].Data?.Name);
    }

    [Fact]
    public async Task ResolveVanityUrlAsync_WithVanityUrl_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<VanityUrlResponse>
        {
            Response = new VanityUrlResponse
            {
                SteamId = "76561197960435530",
                Success = 1,
                Message = "Success"
            }
        };

        SetupMockResponse("/api/steamuser/resolve/ac89?urlType=1", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.ResolveVanityUrlAsync("ac89");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal("76561197960435530", result.Response.SteamId);
        Assert.Equal(1, result.Response.Success);
        Assert.Equal("Success", result.Response.Message);
    }

    [Fact]
    public async Task ResolveVanityUrlAsync_WithFullUrl_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<VanityUrlResponse>
        {
            Response = new VanityUrlResponse
            {
                SteamId = "76561197960435530",
                Success = 1,
                Message = "Success"
            }
        };

        SetupMockResponse("/api/steamuser/resolve/ac89?urlType=1", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.ResolveVanityUrlAsync("https://steamcommunity.com/id/ac89");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal("76561197960435530", result.Response.SteamId);
        Assert.Equal(1, result.Response.Success);
        Assert.Equal("Success", result.Response.Message);
    }

    [Fact]
    public async Task ResolveVanityUrlAsync_WithGroupUrlType_ReturnsValidResponse()
    {
        // Arrange
        var expectedResponse = new SteamResponse<VanityUrlResponse>
        {
            Response = new VanityUrlResponse
            {
                SteamId = "103582791429521412",
                Success = 1,
                Message = "Success"
            }
        };

        SetupMockResponse("/api/steamuser/resolve/valve?urlType=2", expectedResponse);

        var client = CreateClient();

        // Act
        var result = await client.ResolveVanityUrlAsync("valve", 2);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Response);
        Assert.Equal("103582791429521412", result.Response.SteamId);
        Assert.Equal(1, result.Response.Success);
        Assert.Equal("Success", result.Response.Message);
    }

    [Fact]
    public async Task ResolveVanityUrlAsync_WithEmptyInput_ThrowsArgumentException()
    {
        // Arrange
        var client = CreateClient();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => client.ResolveVanityUrlAsync(""));
        Assert.Contains("Vanity URL cannot be null or empty", exception.Message);
    }

    [Fact]
    public void HttpClient_WithApiKey_SetsApiKeyHeader()
    {
        // Arrange
        var apiKey = "test-api-key-12345";
        
        _services.AddSteamApiClient(options =>
        {
            options.BaseUrl = "https://test-api.com";
            options.ApiKey = apiKey;
        });

        var serviceProvider = _services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(typeof(ISteamApiClient).Name);

        // Act
        var hasApiKeyHeader = httpClient.DefaultRequestHeaders.Contains("X-API-Key");
        var apiKeyValue = httpClient.DefaultRequestHeaders.GetValues("X-API-Key").FirstOrDefault();

        // Assert
        Assert.True(hasApiKeyHeader, "X-API-Key header should be present");
        Assert.Equal(apiKey, apiKeyValue);
    }

    [Fact]
    public void HttpClient_WithoutApiKey_DoesNotSetApiKeyHeader()
    {
        // Arrange
        _services.AddSteamApiClient(options =>
        {
            options.BaseUrl = "https://test-api.com";
            options.ApiKey = null; // No API key
        });

        var serviceProvider = _services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(typeof(ISteamApiClient).Name);

        // Act
        var hasApiKeyHeader = httpClient.DefaultRequestHeaders.Contains("X-API-Key");

        // Assert
        Assert.False(hasApiKeyHeader, "X-API-Key header should not be present when no API key is provided");
    }

    [Fact]
    public void HttpClient_WithEmptyApiKey_DoesNotSetApiKeyHeader()
    {
        // Arrange
        _services.AddSteamApiClient(options =>
        {
            options.BaseUrl = "https://test-api.com";
            options.ApiKey = ""; // Empty API key
        });

        var serviceProvider = _services.BuildServiceProvider();
        var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(typeof(ISteamApiClient).Name);

        // Act
        var hasApiKeyHeader = httpClient.DefaultRequestHeaders.Contains("X-API-Key");

        // Assert
        Assert.False(hasApiKeyHeader, "X-API-Key header should not be present when API key is empty");
    }

    private void SetupMockResponse<T>(string expectedUrl, T response)
    {
        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        _mockHttpHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });
    }

    private ISteamApiClient CreateClient()
    {
        var httpClient = new HttpClient(_mockHttpHandler.Object)
        {
            BaseAddress = new Uri("https://test-api.com")
        };

        var options = new SteamApiClientOptions
        {
            BaseUrl = "https://test-api.com",
            ApiKey = "test-api-key"
        };

        var logger = Mock.Of<ILogger<SteamApiClient>>();

        return new SteamApiClient(httpClient, Microsoft.Extensions.Options.Options.Create(options), logger);
    }
} 