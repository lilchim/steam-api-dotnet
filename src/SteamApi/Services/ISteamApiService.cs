using SteamApi.Models;

namespace SteamApi.Services;

/// <summary>
/// Service for making HTTP calls to the Steam Web API
/// </summary>
public interface ISteamApiService
{
    /// <summary>
    /// Makes a GET request to the Steam Web API
    /// </summary>
    /// <param name="interfaceName">Steam API interface name (e.g., "ISteamUser")</param>
    /// <param name="methodName">Steam API method name (e.g., "GetPlayerSummaries")</param>
    /// <param name="version">API version (e.g., "v0002")</param>
    /// <param name="parameters">Query parameters to include in the request</param>
    /// <returns>JSON response as string</returns>
    Task<string> GetAsync(string interfaceName, string methodName, string version, Dictionary<string, string>? parameters = null);
    
    /// <summary>
    /// Makes a GET request to the Steam Web API and deserializes the response
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="interfaceName">Steam API interface name</param>
    /// <param name="methodName">Steam API method name</param>
    /// <param name="version">API version</param>
    /// <param name="parameters">Query parameters to include in the request</param>
    /// <returns>Deserialized response object</returns>
    Task<T> GetAsync<T>(string interfaceName, string methodName, string version, Dictionary<string, string>? parameters = null);
} 