# SteamApi.Models

Data transfer objects (DTOs) for Steam Web API responses.

## Overview

This package provides strongly-typed models for working with Steam Web API responses. It includes models for:

- **Player Information**: User profiles, friends, bans, owned games
- **Game Information**: News, achievements, app lists
- **Response Wrappers**: Standardized response structures
- **Status Information**: API health and configuration status

## Installation

```bash
dotnet add package SteamApi.Models
```

## Usage

These models are designed to work with the Steam Web API responses. They can be used independently or with the [SteamApi.Client](https://www.nuget.org/packages/SteamApi.Client) package.

### Player Models

```csharp
using SteamApi.Models.Steam.Player;
using SteamApi.Models.Steam.Responses;

// Player summary information
var player = new PlayerSummary
{
    SteamId = 76561198000000000,
    PersonaName = "PlayerName",
    ProfileUrl = "https://steamcommunity.com/profiles/76561198000000000",
    Status = PlayerStatus.Online
};

// Player ban information
var bans = new PlayerBans
{
    SteamId = 76561198000000000,
    VacBanned = false,
    NumberOfVacBans = 0,
    CommunityBanned = false
};
```

### Game Models

```csharp
using SteamApi.Models.Steam.Game;
using SteamApi.Models.Steam.Responses;

// Game news
var news = new GameNews
{
    Gid = "123456",
    Title = "Game Update Released",
    Url = "https://steamcommunity.com/games/123456/news/123456",
    Author = "Developer",
    Date = DateTime.UtcNow
};

// Achievement information
var achievement = new Achievement
{
    ApiName = "ACHIEVEMENT_NAME",
    Name = "Achievement Display Name",
    Description = "Achievement description",
    Achieved = true,
    GlobalPercentage = 15.5
};
```

### Response Wrappers

```csharp
using SteamApi.Models.Steam.Responses;

// Wrapped response for player summaries
var response = new SteamResponse<PlayerSummariesResponse>
{
    Response = new PlayerSummariesResponse
    {
        Players = new List<PlayerSummary>
        {
            new PlayerSummary { SteamId = 76561198000000000, PersonaName = "Player1" },
            new PlayerSummary { SteamId = 76561198000000001, PersonaName = "Player2" }
        }
    }
};
```

## Model Categories

### Player Models (`SteamApi.Models.Steam.Player`)
- `PlayerSummary` - Basic user profile information
- `PlayerBans` - User ban information
- `Friend` - Friend list information
- `OwnedGame` - User's owned games
- `RecentlyPlayedGame` - Recently played games

### Game Models (`SteamApi.Models.Steam.Game`)
- `GameNews` - Game news articles
- `Achievement` - Game achievement information
- `SteamApp` - Basic Steam application information

### Response Models (`SteamApi.Models.Steam.Responses`)
- `SteamResponse<T>` - Generic response wrapper
- `PlayerSummariesResponse` - Player summaries endpoint response
- `PlayerBansResponse` - Player bans endpoint response
- `FriendListResponse` - Friend list endpoint response
- `OwnedGamesResponse` - Owned games endpoint response
- `RecentlyPlayedGamesResponse` - Recently played games endpoint response
- `GameNewsResponse` - Game news endpoint response
- `AchievementPercentagesResponse` - Achievement percentages endpoint response
- `AppListResponse` - App list endpoint response

### Status Models (`SteamApi.Models.Status`)
- `StatusResponse` - API status information

## Features

- **Strongly Typed**: All models are strongly typed with proper C# conventions
- **XML Documentation**: Full IntelliSense support with XML documentation
- **Nullable Reference Types**: Proper null handling with nullable reference types
- **Steam API Compatible**: Models match Steam Web API response structures
- **Cross-Platform**: Works on .NET 8+ on any platform
- **No Dependencies**: Zero external dependencies for maximum compatibility

## Related Packages

- **[SteamApi.Client](https://www.nuget.org/packages/SteamApi.Client)** - HTTP client that uses these models
- **[Steam API Service](https://github.com/lilchim/steam-api-dotnet)** - The .NET service that returns these models

## Source Code

- **[GitHub Repository](https://github.com/lilchim/steam-api-dotnet)** - Main repository
- **[GitLab Repository](https://gitlab.com/lilchim/steam-api-dotnet)** - Mirror repository

## License

MIT License - see LICENSE file for details. 