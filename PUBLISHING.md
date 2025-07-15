# Publishing NuGet Packages

This guide explains how to publish the SteamApi packages to nuget.org using GitHub Actions.

## Prerequisites

1. **NuGet.org Account**: Create an account at https://www.nuget.org/
2. **API Key**: Generate an API key in your NuGet account
3. **GitHub Repository**: Push your code to GitHub

## Setup

### 1. Add NuGet API Key to GitHub Secrets

1. Go to your GitHub repository
2. Navigate to **Settings** → **Secrets and variables** → **Actions**
3. Click **New repository secret**
4. Name: `NUGET_API_KEY`
5. Value: Your NuGet API key

### 2. Publishing Process

#### Option A: Automatic Publishing (Recommended)

1. **Create a Release**:
   - Go to your GitHub repository
   - Click **Releases** → **Create a new release**
   - Tag version: `v1.0.0` (or whatever version)
   - Title: `Release v1.0.0`
   - Description: Include `Sgnome.SteamApi.Client` or `Sgnome.SteamApi.Models` to specify which packages to publish
   - Click **Publish release**

2. **GitHub Actions will automatically**:
   - Build the project
   - Run tests
   - Create NuGet packages
   - Publish to nuget.org

#### Option B: Manual Publishing

```bash
# Build and pack
dotnet pack src/SteamApi.Client/SteamApi.Client.csproj --configuration Release
dotnet pack src/SteamApi.Models/SteamApi.Models.csproj --configuration Release

# Publish to NuGet
dotnet nuget push src/SteamApi.Client/bin/Release/Sgnome.SteamApi.Client.1.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
dotnet nuget push src/SteamApi.Models/bin/Release/Sgnome.SteamApi.Models.1.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

## Version Management

### Semantic Versioning

Use semantic versioning for your releases:
- `v1.0.0` - Initial release
- `v1.0.1` - Bug fixes
- `v1.1.0` - New features
- `v2.0.0` - Breaking changes

### Updating Package Versions

Update the version in the `.csproj` files:

```xml
<Version>1.0.0</Version>
```

## GitHub Actions Workflows

### Build Workflow (`build.yml`)
- Runs on every push to `main` and `develop`
- Builds and tests the code
- Creates packages as artifacts
- Uses GitHub's free runners

### Publish Workflow (`publish.yml`)
- Runs when a release is published
- Publishes packages to nuget.org
- Uses GitHub's free runners


## Troubleshooting

### Common Issues

1. **Package name already exists**: Change the `PackageId` in `.csproj`
2. **API key invalid**: Regenerate your NuGet API key
3. **Build fails**: Check the GitHub Actions logs
