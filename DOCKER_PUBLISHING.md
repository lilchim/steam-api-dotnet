# Publishing Docker Images to Docker Hub

This guide explains how to publish Docker images to Docker Hub using GitHub Actions.

## Prerequisites

1. **Docker Hub Account**: Create an account at https://hub.docker.com/
2. **Access Token**: Generate an access token in your Docker Hub account
3. **GitHub Repository**: Push your code to GitHub

## Setup

### 1. Create Docker Hub Access Token

1. **Log in to Docker Hub**
   - Go to https://hub.docker.com/
   - Sign in to your account

2. **Generate Access Token**:
   - Click your username → **Account Settings**
   - Go to **Security** → **New Access Token**
   - **Name**: `github-actions` (or any name you prefer)
   - **Permissions**: Read & Write
   - Click **Generate**
   - **Copy the token** (you won't see it again!)

### 2. Add Docker Hub Credentials to GitHub Secrets

1. **Go to your GitHub repository**
   - Navigate to your repo

2. **Add repository secrets**:
   - Go to **Settings** → **Secrets and variables** → **Actions**
   - Click **New repository secret**
   - **Name**: `DOCKERHUB_USERNAME`
   - **Value**: Your Docker Hub username
   - Click **Add secret**
   
   - Click **New repository secret** again
   - **Name**: `DOCKERHUB_TOKEN`
   - **Value**: Your Docker Hub access token
   - Click **Add secret**

### 3. Publishing Process

#### Automatic Publishing

The Docker image will be published automatically when:
- **Release is published** - Creates tagged images (v1.0.0, v1.0, etc.)
- **Push to main branch** - Creates latest and branch-tagged images

#### Manual Publishing

You can also build and push manually:

```bash
# Build the image
docker build -f docker/steamapi.dockerfile -t lilchim/steam-api-dotnet:latest .

# Push to Docker Hub
docker push lilchim/steam-api-dotnet:latest
```

## Image Tags

The workflow creates multiple tags:

- **`latest`** - Latest build from main branch
- **`v1.0.0`** - Specific version tags
- **`v1.0`** - Major.minor version tags
- **`main-abc123`** - Branch-specific tags with commit SHA

## Using the Published Image

### Pull and Run

```bash
# Pull the image
docker pull yourusername/steam-api-dotnet:latest

# Run the container
docker run -p 5000:80 \
  -e "SteamApi__ApiKey=your-steam-api-key" \
  yourusername/steam-api-dotnet:latest
```

### Docker Compose

```yaml
version: '3.8'

services:
  steamapi:
    image: yourusername/steam-api-dotnet:latest
    ports:
      - "5000:80"
    environment:
      - SteamApi__ApiKey=your-steam-api-key
      - ASPNETCORE_ENVIRONMENT=Production
    restart: unless-stopped
```

## Configuration

### Environment Variables

The Docker image supports these environment variables:

- `SteamApi__ApiKey` - Your Steam API key (required)
- `ASPNETCORE_ENVIRONMENT` - Environment (Development/Production)
- `ASPNETCORE_URLS` - URLs to bind to (default: http://+:80)

### Health Check

The API includes a health check endpoint:
```bash
curl http://localhost:5000/api/status
```

## Troubleshooting

### Common Issues

1. **Authentication failed**: Check your Docker Hub username and token
2. **Build fails**: Check the GitHub Actions logs
3. **Image not found**: Wait a few minutes for indexing

### Docker Hub Repository

Your images will be available at:
- https://hub.docker.com/r/yourusername/steam-api-dotnet
