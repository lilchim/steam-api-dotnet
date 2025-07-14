# Steam API .NET

A .NET implementation of the Steam Web API that provides a clean, secure interface for accessing Steam data. This service acts as a proxy to the official Steam Web API, handling authentication and providing a consistent REST interface.

## Features

- 🔐 **Secure API Key Management** - Server-side Steam API key storage
- 📚 **Comprehensive Documentation** - Full Swagger/OpenAPI documentation
- 🐳 **Docker Support** - Easy deployment with Docker and Docker Compose
- 🔄 **RESTful Interface** - Clean REST endpoints for all Steam API methods
- 📊 **Status Monitoring** - Built-in health checks and configuration status
- 🛡️ **Error Handling** - Robust error handling and logging

## Quick Start

### Option 1: Using Docker Compose (Recommended)

```bash
cd docker
docker-compose up --build
```

### Option 2: Using Docker directly

```bash
docker build -f docker/steamapi.dockerfile -t steamapi .
docker run -p 5000:80 steamapi
```

### Option 3: Local Development

```bash
cd src/SteamApi
dotnet run
```

## Steam API Key Setup

### Obtaining a Steam API Key
Visit [Steam Web API](https://steamcommunity.com/dev/apikey) to request an API key. You do not need a valid domain to request an API key.

### Configuration
A Steam API Key should be added using secrets to avoid accidentally committing to a public repository. Refer to [API_KEY_SETUP.md](API_KEY_SETUP.md) for detailed setup instructions.

## API Documentation

Once running, visit the Swagger UI at `http://localhost:5000/swagger` for interactive API documentation.

## API Endpoints

### Status Endpoints

#### `GET /api/status`
Returns the current status and configuration of the Steam API service.

**Example Response:**
```json
{
  "apiKeyConfigured": true,
  "baseUrl": "https://api.steampowered.com",
  "timeoutSeconds": 30,
  "maxRetries": 3,
  "enableLogging": false,
  "version": "1.0.0",
  "timestamp": "2024-01-15T10:30:00Z",
  "status": "Healthy"
}
```

#### `GET /api/status/health`
Simple health check endpoint.

### Steam News (ISteamNews)

Get news articles and updates for Steam games.

#### `GET /api/steamnews/app/{appId}`
Get news articles for a specific Steam app.

**Parameters:**
- `appId` (path): Steam App ID (e.g., 730 for Counter-Strike 2)
- `count` (query): Number of news items (1-20, default: 20)
- `maxLength` (query): Maximum length of each news item (0 for no limit, default: 0)
- `feeds` (query): Comma-separated list of feed names
- `tags` (query): Comma-separated list of tags to filter by

**Example:**
```bash
# Get 5 news articles for Counter-Strike 2
GET /api/steamnews/app/730?count=5

# Get news with specific tags
GET /api/steamnews/app/730?count=10&tags=update,patch
```

#### `GET /api/steamnews/app/{appId}/authed`
Get news articles with additional features (requires API key).

**Additional Parameters:**
- `endDate` (query): End date for news items (Unix timestamp)
- `days` (query): Number of days to look back (1-365)

**Example:**
```bash
# Get news from the last 7 days
GET /api/steamnews/app/730/authed?count=10&days=7
```

### Steam User Stats (ISteamUserStats)

Access game statistics, achievements, and player counts.

#### `GET /api/steamuserstats/achievements/{gameId}/global`
Get global achievement completion percentages for a game.

**Parameters:**
- `gameId` (path): Steam App ID

**Example:**
```bash
# Get achievement percentages for Portal 2
GET /api/steamuserstats/achievements/620/global
```

**Example Response:**
```json
{
  "achievementpercentages": {
    "achievements": [
      {
        "name": "Portal_Complete_Test_Chambers",
        "percent": 85.2
      }
    ]
  }
}
```

#### `GET /api/steamuserstats/players/{appId}/current`
Get the current number of players for a Steam app.

**Parameters:**
- `appId` (path): Steam App ID

**Example:**
```bash
# Get current player count for Counter-Strike 2
GET /api/steamuserstats/players/730/current
```

**Example Response:**
```json
{
  "response": {
    "player_count": 1234567,
    "result": 1
  }
}
```

#### `GET /api/steamuserstats/achievements/{steamId}/{appId}`
Get achievements for a specific user and game.

**Parameters:**
- `steamId` (path): 64-bit Steam ID
- `appId` (path): Steam App ID
- `language` (query): Language for achievement names (optional)

**Example:**
```bash
# Get user's achievements in Counter-Strike 2
GET /api/steamuserstats/achievements/76561198000000000/730?language=english
```

#### `GET /api/steamuserstats/stats/{steamId}/{appId}`
Get user statistics for a specific game.

**Parameters:**
- `steamId` (path): 64-bit Steam ID
- `appId` (path): Steam App ID

**Example:**
```bash
# Get user's stats in Counter-Strike 2
GET /api/steamuserstats/stats/76561198000000000/730
```

### Steam User (ISteamUser)

Access user profiles, friends, bans, and group information.

#### `GET /api/steamuser/summaries`
Get basic profile information for multiple Steam users.

**Parameters:**
- `steamIds` (query): Comma-delimited list of 64-bit Steam IDs (up to 100)

**Example:**
```bash
# Get profiles for multiple users
GET /api/steamuser/summaries?steamIds=76561198000000000,76561198000000001

# Get single user profile
GET /api/steamuser/summaries?steamIds=76561198000000000
```

**Example Response:**
```json
{
  "response": {
    "players": [
      {
        "steamid": "76561198000000000",
        "personaname": "PlayerName",
        "profileurl": "https://steamcommunity.com/profiles/76561198000000000",
        "avatar": "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/...",
        "personastate": 1,
        "communityvisibilitystate": 3
      }
    ]
  }
}
```

#### `GET /api/steamuser/friends/{steamId}`
Get the friend list of a Steam user.

**Parameters:**
- `steamId` (path): 64-bit Steam ID
- `relationship` (query): Filter by relationship type (all, friend, default: all)

**Example:**
```bash
# Get all friends
GET /api/steamuser/friends/76561198000000000

# Get only confirmed friends
GET /api/steamuser/friends/76561198000000000?relationship=friend
```

#### `GET /api/steamuser/bans`
Get ban information for multiple Steam users.

**Parameters:**
- `steamIds` (query): Comma-delimited list of 64-bit Steam IDs

**Example:**
```bash
# Check bans for multiple users
GET /api/steamuser/bans?steamIds=76561198000000000,76561198000000001
```

**Example Response:**
```json
{
  "players": [
    {
      "SteamId": "76561198000000000",
      "CommunityBanned": false,
      "VACBanned": false,
      "NumberOfVACBans": 0,
      "DaysSinceLastBan": 0,
      "NumberOfGameBans": 0,
      "EconomyBan": "none"
    }
  ]
}
```

#### `GET /api/steamuser/groups/{steamId}`
Get the groups that a Steam user is a member of.

**Parameters:**
- `steamId` (path): 64-bit Steam ID

**Example:**
```bash
# Get user's groups
GET /api/steamuser/groups/76561198000000000
```

#### `GET /api/steamuser/resolve/{vanityUrl}`
Resolve a vanity URL to a Steam ID.

**Parameters:**
- `vanityUrl` (path): The vanity URL to resolve
- `urlType` (query): Type of vanity URL (1 for individual profile, 2 for group, default: 1)

**Example:**
```bash
# Resolve individual profile
GET /api/steamuser/resolve/gabelogannewell

# Resolve group
GET /api/steamuser/resolve/valve?urlType=2
```

**Example Response:**
```json
{
  "response": {
    "steamid": "76561197960435530",
    "success": 1
  }
}
```

### Player Service (IPlayerService)

Access user game libraries, playtime, levels, and badges.

#### `GET /api/player/owned-games/{steamId}`
Get a list of games a player owns along with playtime information.

**Parameters:**
- `steamId` (path): The SteamID of the account
- `includeAppInfo` (query): Include game name and logo information (default: false)
- `includePlayedFreeGames` (query): Include free games the player has played (default: false)
- `appIdsFilter` (query): Comma-separated list of app IDs to filter results

**Example:**
```bash
# Get basic owned games list
GET /api/player/owned-games/76561198000000000

# Get owned games with full app info
GET /api/player/owned-games/76561198000000000?includeAppInfo=true

# Get owned games including free games played
GET /api/player/owned-games/76561198000000000?includeAppInfo=true&includePlayedFreeGames=true

# Filter to specific games
GET /api/player/owned-games/76561198000000000?appIdsFilter=730,570,440
```

**Example Response:**
```json
{
  "response": {
    "game_count": 150,
    "games": [
      {
        "appid": 730,
        "name": "Counter-Strike 2",
        "playtime_forever": 1200,
        "playtime_2weeks": 45,
        "img_icon_url": "07385eb55b5ba974aebbe74d3c99626bda7920b8",
        "has_community_visible_stats": true
      }
    ]
  }
}
```

#### `GET /api/player/recent-games/{steamId}`
Get a list of games a player has played in the last two weeks.

**Parameters:**
- `steamId` (path): The SteamID of the account
- `count` (query): Optionally limit to a certain number of games

**Example:**
```bash
# Get all recently played games
GET /api/player/recent-games/76561198000000000

# Get last 5 recently played games
GET /api/player/recent-games/76561198000000000?count=5
```

#### `GET /api/player/level/{steamId}`
Get the Steam level of a user.

**Parameters:**
- `steamId` (path): The SteamID of the account

**Example:**
```bash
# Get user's Steam level
GET /api/player/level/76561198000000000
```

**Example Response:**
```json
{
  "response": {
    "player_level": 42
  }
}
```

#### `GET /api/player/badges/{steamId}`
Get badges for a user.

**Parameters:**
- `steamId` (path): The SteamID of the account

**Example:**
```bash
# Get user's badges
GET /api/player/badges/76561198000000000
```

#### `GET /api/player/badge-progress/{steamId}`
Get community badge progress for a user.

**Parameters:**
- `steamId` (path): The SteamID of the account
- `badgeId` (query): The badge ID to get progress for (optional)

**Example:**
```bash
# Get all badge progress
GET /api/player/badge-progress/76561198000000000

# Get specific badge progress
GET /api/player/badge-progress/76561198000000000?badgeId=1
```

### Steam Apps (ISteamApps)

Access Steam application information and server data.

#### `GET /api/steamapps/list`
Get a complete list of all Steam applications.

**Example:**
```bash
# Get complete list of all Steam apps
GET /api/steamapps/list
```

**Example Response:**
```json
{
  "applist": {
    "apps": [
      {
        "appid": 730,
        "name": "Counter-Strike 2"
      },
      {
        "appid": 570,
        "name": "Dota 2"
      }
    ]
  }
}
```

#### `GET /api/steamapps/servers/{addr}`
Get servers at a specific IP address.

**Parameters:**
- `addr` (path): IP address to query (IPv4 or IPv6)

**Example:**
```bash
# Get servers at specific IP address
GET /api/steamapps/servers/192.168.1.1

# Get servers at IPv6 address
GET /api/steamapps/servers/2001:db8::1
```

#### `GET /api/steamapps/up-to-date/{appId}`
Check if a Steam app is up to date.

**Parameters:**
- `appId` (path): Steam App ID
- `version` (query): Current version of the app

**Example:**
```bash
# Check if Counter-Strike 2 is up to date
GET /api/steamapps/up-to-date/730?version=12345
```

**Example Response:**
```json
{
  "response": {
    "up_to_date": true,
    "version_is_listable": true,
    "required_version": 12345,
    "message": "App is up to date"
  }
}
```

#### `GET /api/steamapps/list/v2`
Get Steam app list using version 2 of the API.

**Example:**
```bash
# Get app list with v2 API
GET /api/steamapps/list/v2
```

#### `GET /api/steamapps/list/v1`
Get Steam app list using version 1 of the API.

**Example:**
```bash
# Get app list with v1 API
GET /api/steamapps/list/v1
```

## Common Steam App IDs

| Game | App ID |
|------|--------|
| Counter-Strike 2 | 730 |
| Dota 2 | 570 |
| Team Fortress 2 | 440 |
| Portal 2 | 620 |
| Half-Life 2 | 220 |
| Left 4 Dead 2 | 550 |
| Garry's Mod | 4000 |
| Rust | 252490 |
| PUBG: BATTLEGROUNDS | 578080 |

## Error Handling

The API returns standard HTTP status codes:

- `200` - Success
- `400` - Bad Request (invalid parameters)
- `500` - Internal Server Error (Steam API issues)

Error responses include descriptive messages to help with debugging.

## Development

### Project Structure
```
steam-api-dotnet/
├── src/
│   ├── SteamApi/              # Main Web API project
│   ├── SteamApi.Models/       # Shared models 
│   └── SteamApi.Client/       # NuGet client package 
├── docker/                    # Docker configuration
└── docs/                      # Documentation
```

### Adding New Endpoints

1. Create a new controller in `src/SteamApi/Controllers/`
2. Use the `ISteamApiService` to make Steam API calls
3. Add proper validation and error handling
4. Include XML documentation comments for Swagger
5. Update this README with examples

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Update documentation
6. Submit a pull request

## License

This project is open source and available under the [MIT License](LICENSE).

## API Key Authentication Middleware

This project uses a simple API Key authentication middleware to secure API endpoints.

### How It Works
- All API requests (except `/api/status`, `/api/status/health`, and `/swagger`) require a valid API key.
- The API key must be provided in the `X-API-Key` header (default) or as a `api_key` query parameter.
- Requests without a valid API key will receive a `401 Unauthorized` response.
- Rate limiting is enforced per API key (configurable).

### Configuration
- **API keys are NOT stored in source code or committed to Git.**
- API keys are configured via [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) (for development) or environment variables (for production).
- Example User Secrets setup:
  ```sh
  dotnet user-secrets set "ApiKey:ValidApiKeys:0" "your-dev-key"
  dotnet user-secrets set "ApiKey:ValidApiKeys:1" "another-key"
  ```
- To require API key authentication in development, set `"RequireApiKey": true` in `appsettings.Development.json`.
- Rate limiting and header/query parameter names are configurable in `appsettings.json`.

### Using the API Key
- Add your API key to the `X-API-Key` header in requests:
  ```http
  X-API-Key: your-dev-key
  ```
- Or as a query parameter:
  ```
  GET /api/steamuser/summaries?steamIds=...&api_key=your-dev-key
  ```
- In Swagger UI, use the "Authorize" button and enter your API key.

### Security Best Practices
- **Never commit API keys to source control.**
- Use environment variables or a secrets manager in production.
- Rotate and revoke API keys as needed.
- For advanced scenarios, integrate with your user management/auth service to generate and manage API keys per user.

## CORS Configuration

Cross-Origin Resource Sharing (CORS) is configurable via `appsettings.json` to control which domains can access your API.

### Configuration Options
- **Enabled**: Enable/disable CORS globally
- **AllowedOrigins**: List of domains that can access the API
- **AllowedMethods**: HTTP methods allowed (GET, POST, etc.)
- **AllowedHeaders**: Headers that can be sent with requests
- **AllowCredentials**: Whether to allow cookies/authorization headers
- **PreflightMaxAge**: How long to cache preflight requests

### Example Configuration
```json
{
  "Cors": {
    "Enabled": true,
    "AllowedOrigins": [
      "http://localhost:3000",
      "https://yourdomain.com"
    ],
    "AllowedMethods": ["GET", "POST", "OPTIONS"],
    "AllowedHeaders": ["Content-Type", "X-API-Key"],
    "AllowCredentials": false
  }
}
```

### Development vs Production
- **Development**: CORS is enabled by default with common localhost origins
- **Production**: CORS is disabled by default - enable and configure specific origins as needed

---

