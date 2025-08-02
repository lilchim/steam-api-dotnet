# SteamApi.Client

Client library for Steam API .NET service.

## Overview

This package provides a strongly-typed HTTP client for interacting with the Steam API .NET service. It includes:

- **Type-safe API calls** - All methods return strongly-typed models from [SteamApi.Models](https://www.nuget.org/packages/SteamApi.Models)
- **Dependency injection support** - Easy integration with .NET applications
- **Configurable options** - Customize base URL, timeouts, API keys, and more
- **Logging support** - Built-in request/response logging
- **Cancellation support** - All methods support cancellation tokens
- **Docker support** - Works with the [Docker container](https://hub.docker.com/r/lilchim/steam-api-dotnet)

## Installation

```bash
dotnet add package SteamApi.Client
```

## Quick Start

### Basic Usage

```csharp
using SteamApi.Client;
using SteamApi.Client.Extensions;

// Register the client
services.AddSteamApiClient(options =>
{
    options.BaseUrl = "https://your-steam-api.com";
    options.ApiKey = "your-api-key";
});

// Use the client
public class MyService
{
    private readonly ISteamApiClient _steamClient;

    public MyService(ISteamApiClient steamClient)
    {
        _steamClient = steamClient;
    }

    public async Task GetPlayerInfo()
    {
        var playerSummaries = await _steamClient.GetPlayerSummariesAsync("76561198000000000");
        var player = playerSummaries.Response?.Players.FirstOrDefault();
        
        Console.WriteLine($"Player: {player?.PersonaName}");
    }
}
```

### Using with Docker

If you're running the Steam API service in Docker:

```csharp
services.AddSteamApiClient(options =>
{
    options.BaseUrl = "http://localhost:5000"; // Docker container port
    options.ApiKey = "your-api-key";
});
```

```bash
# Run the Docker container
docker run -p 5000:80 -e "SteamApi__ApiKey=your-api-key" lilchim/steam-api-dotnet:latest
```

### Configuration from appsettings.json

```json
{
  "SteamApiClient": {
    "BaseUrl": "https://your-steam-api.com",
    "ApiKey": "your-api-key",
    "TimeoutSeconds": 30,
    "EnableLogging": true
  }
}
```

```csharp
// Register with configuration
services.AddSteamApiClient(configuration);
```

## API Methods

### Player Information

```csharp
// Get player summaries
var summaries = await client.GetPlayerSummariesAsync("76561198000000000,76561198000000001");

// Get friend list
var friends = await client.GetFriendListAsync("76561198000000000");

// Get player bans
var bans = await client.GetPlayerBansAsync("76561198000000000");
```

### Game Information

```csharp
// Get owned games
var ownedGames = await client.GetOwnedGamesAsync("76561198000000000", includeAppInfo: true);

// Get recently played games
var recentGames = await client.GetRecentlyPlayedGamesAsync("76561198000000000", count: 10);

// Get game news
var news = await client.GetNewsForAppAsync(730, count: 5); // Counter-Strike 2

// Get achievement percentages
var achievements = await client.GetGlobalAchievementPercentagesForAppAsync(730);

// Get app list
var apps = await client.GetAppListAsync();
```

### Store Information

```csharp
// Get store details for a single app
var appDetails = await client.GetStoreAppDetailsAsync(238960); // Path of Exile

// Get store details for multiple apps
var multipleAppDetails = await client.GetStoreAppDetailsMultipleAsync("238960,730,440"); // Path of Exile, CS2, TF2

// Access store information
var app = appDetails["238960"];
Console.WriteLine($"App: {app.Data?.Name}");
Console.WriteLine($"Type: {app.Data?.Type}");
Console.WriteLine($"Is Free: {app.Data?.IsFree}");
Console.WriteLine($"Developers: {string.Join(", ", app.Data?.Developers ?? new List<string>())}");
```

### Status

```csharp
// Get API status
var status = await client.GetStatusAsync();
Console.WriteLine($"API Status: {status.Status}");
```

## Configuration Options

### SteamApiClientOptions

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `BaseUrl` | string | "http://localhost:5000" | Base URL of the Steam API service |
| `ApiKey` | string? | null | API key for authentication |
| `TimeoutSeconds` | int | 30 | HTTP request timeout in seconds |
| `MaxRetries` | int | 3 | Maximum number of retry attempts (planned for future releases) |
| `EnableLogging` | bool | false | Whether to enable request/response logging |
| `UserAgent` | string | "SteamApi.Client/1.0.1" | User agent string for HTTP requests |
| `Cache` | CacheOptions | new() | Cache configuration |

### CacheOptions

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Enabled` | bool | false | Whether caching is enabled |
| `Type` | CacheType | Memory | Cache type to use |
| `DefaultTtlMinutes` | int | 15 | Default time-to-live for cached items |
| `RedisConnectionString` | string? | null | Redis connection string |

> **Note**: Caching functionality is planned for future releases. The cache options are currently available for configuration but not yet implemented.

## Error Handling

The client throws standard .NET exceptions:

- `HttpRequestException` - Network or HTTP errors
- `JsonException` - JSON deserialization errors
- `InvalidOperationException` - Configuration or deserialization errors

```csharp
try
{
    var result = await client.GetPlayerSummariesAsync("76561198000000000");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP error: {ex.Message}");
}
catch (JsonException ex)
{
    Console.WriteLine($"JSON error: {ex.Message}");
}
```

## Logging

Enable request/response logging:

```csharp
services.AddSteamApiClient(options =>
{
    options.BaseUrl = "https://your-steam-api.com";
    options.EnableLogging = true;
});
```

## Dependencies

- **[SteamApi.Models](https://www.nuget.org/packages/SteamApi.Models)** - Data transfer objects
- **Microsoft.Extensions.Http** - HTTP client factory
- **Microsoft.Extensions.Options** - Configuration options
- **Microsoft.Extensions.Logging.Abstractions** - Logging support
- **Microsoft.Extensions.Configuration** - Configuration binding
- **Microsoft.Extensions.Configuration.Binder** - Configuration binding support

## Related Packages

- **[SteamApi.Models](https://www.nuget.org/packages/SteamApi.Models)** - Data transfer objects used by this client
- **[Steam API Service](https://github.com/lilchim/steam-api-dotnet)** - The .NET service that this client connects to
- **[Docker Image](https://hub.docker.com/r/lilchim/steam-api-dotnet)** - Docker container for the Steam API service

## Source Code

- **[GitHub Repository](https://github.com/lilchim/steam-api-dotnet)** - Main repository

## License

MIT License - see LICENSE file for details. 