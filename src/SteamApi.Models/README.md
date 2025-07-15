# SteamApi.Models

Data transfer objects (DTOs) for Steam Web API responses.

## Overview

This package provides strongly-typed models for working with Steam Web API responses. It includes models for:

- **Player Information**: User profiles, friends, bans, owned games
- **Game Information**: News, achievements, app lists
- **Response Wrappers**: Standardized response structures

## Installation

```bash
dotnet add package Sgnome.SteamApi.Models
```

## Usage

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
- **Cross-Platform**: Works on .NET 6+ on any platform

## License

MIT License - see LICENSE file for details. 