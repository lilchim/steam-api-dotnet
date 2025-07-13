# steam-api-dotnet
Dockerized Dotnet implementation of the Steam API

## Getting Started

Option 1: Using Docker Compose

```
cd docker
docker-compose up --build
```

Option 2: Using Docker directly
```
docker build -f docker/steamapi.dockerfile -t steamapi .
docker run -p 5000:80 steamapi
```

## Steam API Key 
### Obtaining
https://steamcommunity.com/dev/apikey

You do not need a valid domain to request an API key

### Usage
A Steam API Key should be added using secrets to avoid accidentally committing to a public repository. Refer to API_KEY_SETUP.md for help and examples.

