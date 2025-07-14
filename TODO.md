# Steam API .NET Project TODO

## Phase 1: Core API Foundation

### Project Structure Setup
- [x] Create solution file (`steam-api-dotnet.sln`)
- [x] Create `SteamApi.Models` class library project
- [x] Create `SteamApi.Client` class library project
- [x] Move existing models from `SteamApi` to `SteamApi.Models`
- [x] Add project references between projects

### Steam API Implementation
- [x] Research Steam Web API documentation and endpoints
- [x] Decide which Steam API endpoints to support initially
  - [x] ISteamNews - News feeds for Steam games
  - [x] ISteamUserStats - Global stat information by game
  - [x] ISteamUser - Information about Steam users
  - [ ] ITFItems_440 - Team Fortress 2 items (SKIPPED - not needed)
- [x] Implement Steam API key configuration (appsettings.json, environment variables)
- [x] Implement Status endpoint for checking web api configuration
- [x] Create base HTTP client for Steam API calls
- [x] Implement error handling
- [ ] Implement retry logic (using Polly or similar library)
- [x] Add logging for API calls

### API Endpoints
- [x] Design REST API endpoints for your Steam API proxy
- [x] Enable XML Document Generation for Swagger
- [ ] Implement controllers for each Steam API interface:
  - [x] SteamNewsController - ISteamNews endpoints
  - [x] SteamUserStatsController - ISteamUserStats endpoints  
  - [ ] SteamUserController - ISteamUser endpoints
  - [ ] CustomController - Your own useful API methods
- [ ] Add proper HTTP status codes and error responses
- [ ] Implement request/response models
- [ ] Add input validation

### Security & Configuration
- [ ] Implement API key security (how clients authenticate to your API)
- [ ] Add rate limiting
- [ ] Configure CORS if needed
- [ ] Set up proper environment configuration
- [ ] Add health check endpoints

## Phase 2: NuGet Package Development

### SteamApi.Models Package
- [ ] Create all Steam API response DTOs
- [ ] Add XML documentation to all models
- [ ] Configure package metadata (version, description, etc.)
- [ ] Test package locally with `dotnet pack`
- [ ] Publish to NuGet

### SteamApi.Client Package
- [ ] Design the client interface (`ISteamApiClient`)
- [ ] Implement `SteamApiClient` class
- [ ] Add dependency injection extensions (`AddSteamApiClient`)
- [ ] Implement configuration options class
- [ ] Add retry policies and error handling
- [ ] Add XML documentation
- [ ] Create usage examples
- [ ] Test client with real API calls
- [ ] Publish to NuGet

### Client Features
- [ ] Implement all client methods matching your API endpoints
- [ ] Add async/await support throughout
- [ ] Implement proper disposal of HTTP resources
- [ ] Add request/response logging
- [ ] Implement caching strategies if needed

## Phase 3: TypeScript/JavaScript Generation

### Decision: TypeScript Generation Tool
**Choose one approach:**
- [ ] **NSwag** (Recommended)
  - Pros: Integrates well with ASP.NET Core, generates Angular/React services
  - Cons: Limited to .NET ecosystem
- [ ] **OpenAPI Generator**
  - Pros: Language agnostic, supports 50+ languages
  - Cons: More complex setup, separate toolchain
- [ ] **Custom Source Generator**
  - Pros: Full control, integrated build process
  - Cons: More development time, maintenance overhead

### Implementation
- [ ] Set up chosen TypeScript generation tool
- [ ] Configure generation to output TypeScript interfaces
- [ ] Generate service classes for HTTP calls
- [ ] Create npm package structure
- [ ] Add TypeScript configuration files
- [ ] Create usage examples for TypeScript/JavaScript
- [ ] Set up automated generation in build process

## Phase 4: Documentation & Examples

### API Documentation
- [ ] Enhance Swagger documentation with examples
- [ ] Add detailed API documentation
- [ ] Create getting started guide
- [ ] Document authentication methods
- [ ] Add troubleshooting guide

### Code Examples
- [ ] Create .NET usage examples
- [ ] Create TypeScript/JavaScript usage examples
- [ ] Create Docker deployment examples
- [ ] Add integration tests
- [ ] Create sample applications

## Phase 5: DevOps & Publishing

### Docker
- [ ] Optimize Dockerfile for production
- [ ] Create multi-architecture builds (ARM64, AMD64)
- [ ] Set up Docker Hub automated builds
- [ ] Create docker-compose examples

### CI/CD
- [ ] Set up GitHub Actions for automated builds
- [ ] Configure automated testing
- [ ] Set up automated NuGet publishing
- [ ] Set up automated Docker image publishing
- [ ] Add automated TypeScript generation

### Monitoring & Observability
- [ ] Add application metrics
- [ ] Implement structured logging
- [ ] Add performance monitoring
- [ ] Set up error tracking

## Technical Decisions to Make

### Architecture Decisions
- [ ] **Authentication Method**: API keys vs JWT vs OAuth
- [ ] **Rate Limiting Strategy**: In-memory vs Redis vs database
- [ ] **Caching Strategy**: Memory cache vs distributed cache
- [ ] **Error Handling**: Custom exceptions vs standard HTTP errors
- [ ] **Logging Strategy**: Structured logging format and levels

### Package Decisions
- [ ] **NuGet Package Structure**: Single package vs multiple packages
- [ ] **Versioning Strategy**: Semantic versioning approach
- [ ] **Package Dependencies**: Which .NET packages to include/exclude
- [ ] **Target Frameworks**: .NET 6 only vs multi-target (.NET 6, .NET 8)

### API Design Decisions
- [ ] **Endpoint Naming**: RESTful vs RPC-style
- [ ] **Response Format**: JSON structure and naming conventions
- [ ] **Pagination**: How to handle large result sets
- [ ] **Filtering**: Query parameter vs request body filtering
- [ ] **Error Response Format**: Standard error response structure

### TypeScript Generation Decisions
- [ ] **Generation Tool**: NSwag vs OpenAPI Generator vs Custom
- [ ] **Output Format**: Interfaces only vs full service classes
- [ ] **Package Distribution**: npm package vs GitHub releases
- [ ] **Version Synchronization**: How to keep .NET and TypeScript in sync

## Immediate Next Steps (Priority Order)

1. **Set up project structure** - Create the solution and project files
2. **Choose initial endpoints** - Pick 2-3 core endpoints to start with:
   - [ ] ISteamUser/GetPlayerSummaries - Basic user profile info
   - [ ] ISteamUserStats/GetGlobalAchievementPercentagesForApp - Achievement stats
   - [ ] ISteamNews/GetNewsForApp - Game news feeds
3. **Implement basic Steam API client** - Get one endpoint working end-to-end
4. **Design your API interface** - Plan how other services will consume your API

## Steam API Interface Details

### ISteamUser
- GetPlayerSummaries - Basic profile info (avatar, status, etc.)
- GetFriendList - User's friends list
- GetPlayerBans - VAC bans, game bans, etc.
- GetUserGroupList - Groups user belongs to

### ISteamUserStats  
- GetGlobalAchievementPercentagesForApp - Achievement completion rates
- GetGlobalStatsForGame - Global game statistics
- GetNumberOfCurrentPlayers - Current player count
- GetPlayerAchievements - User's achievements for a game
- GetUserStatsForGame - User's stats for a game

### ISteamNews
- GetNewsForApp - News articles for a specific game
- GetNewsForAppAuthed - News with additional features (requires API key)

### Custom Controller Ideas
- [ ] GetUserGameLibrary - Combine multiple Steam API calls
- [ ] GetGameDetails - Enhanced game info with stats
- [ ] GetUserActivity - Recent activity summary
- [ ] GetPopularGames - Trending games based on player count

## Questions to Answer

- [x] Which Steam API endpoints are most important for your use case?
  - [x] ISteamNews - News feeds for Steam games
  - [x] ISteamUserStats - Global stat information by game  
  - [x] ISteamUser - Information about Steam users
  - [x] ITFItems_440 - Team Fortress 2 items (SKIPPED)
- [ ] How do you want other services to authenticate to your API?
- [ ] Do you need real-time data or is cached data acceptable?
- [ ] What's your target audience? (Internal services only vs public API)
- [ ] How do you want to handle Steam API rate limits?
- [ ] What's your preferred TypeScript generation approach?

---

