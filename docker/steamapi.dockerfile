# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["src/SteamApi/SteamApi.csproj", "src/SteamApi/"]
RUN dotnet restore "src/SteamApi/SteamApi.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/src/SteamApi"
RUN dotnet build "SteamApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "SteamApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copy published application
COPY --from=publish /app/publish .

# Expose port 80
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "SteamApi.dll"]
