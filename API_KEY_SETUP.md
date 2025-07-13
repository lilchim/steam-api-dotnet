# Steam API Key Setup

This document explains how to safely configure your Steam API key for different environments.

## Development (Local)

### Using User Secrets (Recommended)

1. **Initialize User Secrets** (run once per project):
   ```bash
   cd src/SteamApi
   dotnet user-secrets init
   ```

2. **Set your Steam API Key**:
   ```bash
   dotnet user-secrets set "SteamApi:ApiKey" "your-actual-steam-api-key"
   ```

3. **Verify it's set**:
   ```bash
   dotnet user-secrets list
   ```

### Using Environment Variables

Alternatively, you can set environment variables:

**Windows:**
```cmd
set SteamApi__ApiKey=your-actual-steam-api-key
```

**Linux/macOS:**
```bash
export SteamApi__ApiKey=your-actual-steam-api-key
```

## Production

### Environment Variables

Set the environment variable in your production environment:

```bash
export SteamApi__ApiKey=your-actual-steam-api-key
```

### Docker

Pass the environment variable when running the container:

```bash
docker run -e SteamApi__ApiKey=your-actual-steam-api-key your-image-name
```

Or in docker-compose:
```yaml
services:
  steamapi:
    environment:
      - SteamApi__ApiKey=${STEAM_API_KEY}
```

## CI/CD (GitHub Actions)

1. **Add Secret to GitHub Repository**:
   - Go to your repository → Settings → Secrets and variables → Actions
   - Click "New repository secret"
   - Name: `STEAM_API_KEY`
   - Value: Your Steam API key

2. **Use in Workflow**:
   ```yaml
   - name: Deploy
     env:
       STEAM_API_KEY: ${{ secrets.STEAM_API_KEY }}
   ```

## Getting a Steam API Key

1. Visit [Steam Web API](https://steamcommunity.com/dev)
2. Fill out the form to request an API key
3. Agree to the Steam API Terms of Use
4. You'll receive your API key via email

## Security Notes

- ✅ **User Secrets** are stored outside your project folder and are automatically ignored by Git
- ✅ **Environment Variables** are not stored in source code
- ✅ **GitHub Secrets** are encrypted and only accessible during workflow runs
- ❌ **Never commit API keys** to source control
- ❌ **Never hardcode API keys** in appsettings.json

## Configuration Hierarchy

The application reads configuration in this order (later values override earlier ones):

1. `appsettings.json` (default values)
2. `appsettings.{Environment}.json` (environment-specific)
3. User Secrets (development only)
4. Environment Variables
5. Command line arguments

This means environment variables will always override values in appsettings.json, making them perfect for sensitive data like API keys. 