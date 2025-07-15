using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SteamApi.Client.Configuration;

namespace SteamApi.Client.Extensions;

/// <summary>
/// Extension methods for configuring Steam API client services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Steam API client to the service collection with default configuration
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddSteamApiClient(this IServiceCollection services)
    {
        return services.AddSteamApiClient(options => { });
    }

    /// <summary>
    /// Adds the Steam API client to the service collection with custom configuration
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configureOptions">Action to configure the client options</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddSteamApiClient(this IServiceCollection services, Action<SteamApiClientOptions> configureOptions)
    {
        // Configure options
        services.Configure(configureOptions);

        // Add HTTP client
        services.AddHttpClient<ISteamApiClient, SteamApiClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SteamApiClientOptions>>().Value;
            
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            
            // Add API key header if provided
            if (!string.IsNullOrEmpty(options.ApiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-Key", options.ApiKey);
            }
        });

        return services;
    }

    /// <summary>
    /// Adds the Steam API client to the service collection with configuration from appsettings
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">The configuration</param>
    /// <param name="sectionName">The configuration section name (default: "SteamApiClient")</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddSteamApiClient(this IServiceCollection services, IConfiguration configuration, string sectionName = "SteamApiClient")
    {
        // Configure options from configuration
        services.Configure<SteamApiClientOptions>(options =>
        {
            configuration.GetSection(sectionName).Bind(options);
        });

        // Add HTTP client
        services.AddHttpClient<ISteamApiClient, SteamApiClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SteamApiClientOptions>>().Value;
            
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            
            // Add API key header if provided
            if (!string.IsNullOrEmpty(options.ApiKey))
            {
                client.DefaultRequestHeaders.Add("X-API-Key", options.ApiKey);
            }
        });

        return services;
    }
} 