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
│   ├── SteamApi.Models/       # Shared models (future)
│   └── SteamApi.Client/       # NuGet client package (future)
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

