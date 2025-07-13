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

## Settings