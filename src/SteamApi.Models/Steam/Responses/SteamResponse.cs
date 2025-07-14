namespace SteamApi.Models.Steam.Responses;

/// <summary>
/// Base response wrapper for Steam API responses
/// </summary>
/// <typeparam name="T">The type of the response data</typeparam>
public class SteamResponse<T>
{
    /// <summary>
    /// The response data
    /// </summary>
    public T? Response { get; set; }
} 