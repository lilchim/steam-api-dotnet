using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Store;

/// <summary>
/// Response model for Steam Store API app details
/// </summary>
public class StoreAppDetailsResponse
{
    /// <summary>
    /// Whether the request was successful
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    /// <summary>
    /// The app details data
    /// </summary>
    [JsonPropertyName("data")]
    public StoreAppDetails? Data { get; set; }
}

/// <summary>
/// Steam Store app details
/// </summary>
public class StoreAppDetails
{
    /// <summary>
    /// The type of the app (game, dlc, etc.)
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// The name of the app
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The Steam app ID
    /// </summary>
    [JsonPropertyName("steam_appid")]
    public int SteamAppId { get; set; }
    
    /// <summary>
    /// Required age to play
    /// </summary>
    [JsonPropertyName("required_age")]
    public int RequiredAge { get; set; }
    
    /// <summary>
    /// Whether the app is free
    /// </summary>
    [JsonPropertyName("is_free")]
    public bool IsFree { get; set; }
    
    /// <summary>
    /// Controller support level
    /// </summary>
    [JsonPropertyName("controller_support")]
    public string? ControllerSupport { get; set; }
    
    /// <summary>
    /// List of DLC app IDs
    /// </summary>
    [JsonPropertyName("dlc")]
    public List<int>? Dlc { get; set; }
    
    /// <summary>
    /// Detailed description of the app
    /// </summary>
    [JsonPropertyName("detailed_description")]
    public string? DetailedDescription { get; set; }
    
    /// <summary>
    /// About the game section
    /// </summary>
    [JsonPropertyName("about_the_game")]
    public string? AboutTheGame { get; set; }
    
    /// <summary>
    /// Short description of the app
    /// </summary>
    [JsonPropertyName("short_description")]
    public string? ShortDescription { get; set; }
    
    /// <summary>
    /// Supported languages
    /// </summary>
    [JsonPropertyName("supported_languages")]
    public string? SupportedLanguages { get; set; }
    
    /// <summary>
    /// Reviews summary
    /// </summary>
    [JsonPropertyName("reviews")]
    public string? Reviews { get; set; }
    
    /// <summary>
    /// Header image URL
    /// </summary>
    [JsonPropertyName("header_image")]
    public string? HeaderImage { get; set; }
    
    /// <summary>
    /// Capsule image URL
    /// </summary>
    [JsonPropertyName("capsule_image")]
    public string? CapsuleImage { get; set; }
    
    /// <summary>
    /// Capsule image v5 URL
    /// </summary>
    [JsonPropertyName("capsule_imagev5")]
    public string? CapsuleImageV5 { get; set; }
    
    /// <summary>
    /// Website URL
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }
    
    /// <summary>
    /// PC requirements
    /// </summary>
    [JsonPropertyName("pc_requirements")]
    [JsonConverter(typeof(StoreRequirementsConverter))]
    public StoreRequirements? PcRequirements { get; set; }
    
    /// <summary>
    /// Mac requirements
    /// </summary>
    [JsonPropertyName("mac_requirements")]
    [JsonConverter(typeof(StoreRequirementsConverter))]
    public StoreRequirements? MacRequirements { get; set; }
    
    /// <summary>
    /// Linux requirements
    /// </summary>
    [JsonPropertyName("linux_requirements")]
    [JsonConverter(typeof(StoreRequirementsConverter))]
    public StoreRequirements? LinuxRequirements { get; set; }
    
    /// <summary>
    /// Legal notice
    /// </summary>
    [JsonPropertyName("legal_notice")]
    public string? LegalNotice { get; set; }
    
    /// <summary>
    /// List of developers
    /// </summary>
    [JsonPropertyName("developers")]
    public List<string>? Developers { get; set; }
    
    /// <summary>
    /// List of publishers
    /// </summary>
    [JsonPropertyName("publishers")]
    public List<string>? Publishers { get; set; }
    
    /// <summary>
    /// List of package IDs
    /// </summary>
    [JsonPropertyName("packages")]
    public List<int>? Packages { get; set; }
    
    /// <summary>
    /// Package groups
    /// </summary>
    [JsonPropertyName("package_groups")]
    public List<StorePackageGroup>? PackageGroups { get; set; }
    
    /// <summary>
    /// Supported platforms
    /// </summary>
    [JsonPropertyName("platforms")]
    public StorePlatforms? Platforms { get; set; }
    
    /// <summary>
    /// Metacritic information
    /// </summary>
    [JsonPropertyName("metacritic")]
    public StoreMetacritic? Metacritic { get; set; }
    
    /// <summary>
    /// List of categories
    /// </summary>
    [JsonPropertyName("categories")]
    public List<StoreCategory>? Categories { get; set; }
    
    /// <summary>
    /// List of genres
    /// </summary>
    [JsonPropertyName("genres")]
    public List<StoreGenre>? Genres { get; set; }
    
    /// <summary>
    /// List of screenshots
    /// </summary>
    [JsonPropertyName("screenshots")]
    public List<StoreScreenshot>? Screenshots { get; set; }
    
    /// <summary>
    /// List of movies
    /// </summary>
    [JsonPropertyName("movies")]
    public List<StoreMovie>? Movies { get; set; }
    
    /// <summary>
    /// Recommendations information
    /// </summary>
    [JsonPropertyName("recommendations")]
    public StoreRecommendations? Recommendations { get; set; }
    
    /// <summary>
    /// Achievements information
    /// </summary>
    [JsonPropertyName("achievements")]
    public StoreAchievements? Achievements { get; set; }
    
    /// <summary>
    /// Release date information
    /// </summary>
    [JsonPropertyName("release_date")]
    public StoreReleaseDate? ReleaseDate { get; set; }
    
    /// <summary>
    /// Support information
    /// </summary>
    [JsonPropertyName("support_info")]
    public StoreSupportInfo? SupportInfo { get; set; }
    
    /// <summary>
    /// Background image URL
    /// </summary>
    [JsonPropertyName("background")]
    public string? Background { get; set; }
    
    /// <summary>
    /// Raw background image URL
    /// </summary>
    [JsonPropertyName("background_raw")]
    public string? BackgroundRaw { get; set; }
    
    /// <summary>
    /// Content descriptors
    /// </summary>
    [JsonPropertyName("content_descriptors")]
    public StoreContentDescriptors? ContentDescriptors { get; set; }
    
    /// <summary>
    /// Ratings information
    /// </summary>
    [JsonPropertyName("ratings")]
    public Dictionary<string, StoreRating>? Ratings { get; set; }
}

/// <summary>
/// Store requirements for a platform
/// </summary>
public class StoreRequirements
{
    /// <summary>
    /// Minimum requirements
    /// </summary>
    [JsonPropertyName("minimum")]
    public string? Minimum { get; set; }
    
    /// <summary>
    /// Recommended requirements
    /// </summary>
    [JsonPropertyName("recommended")]
    public string? Recommended { get; set; }
}

/// <summary>
/// Store package group
/// </summary>
public class StorePackageGroup
{
    /// <summary>
    /// Package group name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Package group title
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Package group description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Selection text
    /// </summary>
    [JsonPropertyName("selection_text")]
    public string SelectionText { get; set; } = string.Empty;
    
    /// <summary>
    /// Save text
    /// </summary>
    [JsonPropertyName("save_text")]
    public string SaveText { get; set; } = string.Empty;
    
    /// <summary>
    /// Display type
    /// </summary>
    [JsonPropertyName("display_type")]
    public int DisplayType { get; set; }
    
    /// <summary>
    /// Whether it's a recurring subscription
    /// </summary>
    [JsonPropertyName("is_recurring_subscription")]
    public string IsRecurringSubscription { get; set; } = string.Empty;
    
    /// <summary>
    /// Subscriptions
    /// </summary>
    [JsonPropertyName("subs")]
    public List<StoreSubscription>? Subs { get; set; }
}

/// <summary>
/// Store subscription
/// </summary>
public class StoreSubscription
{
    /// <summary>
    /// Package ID
    /// </summary>
    [JsonPropertyName("packageid")]
    public int PackageId { get; set; }
    
    /// <summary>
    /// Percent savings text
    /// </summary>
    [JsonPropertyName("percent_savings_text")]
    public string PercentSavingsText { get; set; } = string.Empty;
    
    /// <summary>
    /// Percent savings
    /// </summary>
    [JsonPropertyName("percent_savings")]
    public int PercentSavings { get; set; }
    
    /// <summary>
    /// Option text
    /// </summary>
    [JsonPropertyName("option_text")]
    public string OptionText { get; set; } = string.Empty;
    
    /// <summary>
    /// Option description
    /// </summary>
    [JsonPropertyName("option_description")]
    public string OptionDescription { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether can get free license
    /// </summary>
    [JsonPropertyName("can_get_free_license")]
    public string CanGetFreeLicense { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether it's a free license
    /// </summary>
    [JsonPropertyName("is_free_license")]
    public bool IsFreeLicense { get; set; }
    
    /// <summary>
    /// Price in cents with discount
    /// </summary>
    [JsonPropertyName("price_in_cents_with_discount")]
    public int PriceInCentsWithDiscount { get; set; }
}

/// <summary>
/// Store platforms
/// </summary>
public class StorePlatforms
{
    /// <summary>
    /// Windows support
    /// </summary>
    [JsonPropertyName("windows")]
    public bool Windows { get; set; }
    
    /// <summary>
    /// Mac support
    /// </summary>
    [JsonPropertyName("mac")]
    public bool Mac { get; set; }
    
    /// <summary>
    /// Linux support
    /// </summary>
    [JsonPropertyName("linux")]
    public bool Linux { get; set; }
}

/// <summary>
/// Store metacritic information
/// </summary>
public class StoreMetacritic
{
    /// <summary>
    /// Metacritic score
    /// </summary>
    [JsonPropertyName("score")]
    public int Score { get; set; }
    
    /// <summary>
    /// Metacritic URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

/// <summary>
/// Store category
/// </summary>
public class StoreCategory
{
    /// <summary>
    /// Category ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Category description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Store genre
/// </summary>
public class StoreGenre
{
    /// <summary>
    /// Genre ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Genre description
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Store screenshot
/// </summary>
public class StoreScreenshot
{
    /// <summary>
    /// Screenshot ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Thumbnail path
    /// </summary>
    [JsonPropertyName("path_thumbnail")]
    public string PathThumbnail { get; set; } = string.Empty;
    
    /// <summary>
    /// Full path
    /// </summary>
    [JsonPropertyName("path_full")]
    public string PathFull { get; set; } = string.Empty;
}

/// <summary>
/// Store movie
/// </summary>
public class StoreMovie
{
    /// <summary>
    /// Movie ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Movie name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Thumbnail URL
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; } = string.Empty;
    
    /// <summary>
    /// WebM formats
    /// </summary>
    [JsonPropertyName("webm")]
    public StoreMovieFormats? Webm { get; set; }
    
    /// <summary>
    /// MP4 formats
    /// </summary>
    [JsonPropertyName("mp4")]
    public StoreMovieFormats? Mp4 { get; set; }
    
    /// <summary>
    /// Whether this is a highlight
    /// </summary>
    [JsonPropertyName("highlight")]
    public bool Highlight { get; set; }
}

/// <summary>
/// Store movie formats
/// </summary>
public class StoreMovieFormats
{
    /// <summary>
    /// 480p format
    /// </summary>
    [JsonPropertyName("480")]
    public string? Format480 { get; set; }
    
    /// <summary>
    /// Maximum quality format
    /// </summary>
    [JsonPropertyName("max")]
    public string? Max { get; set; }
}

/// <summary>
/// Store recommendations
/// </summary>
public class StoreRecommendations
{
    /// <summary>
    /// Total number of recommendations
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
}

/// <summary>
/// Store achievements
/// </summary>
public class StoreAchievements
{
    /// <summary>
    /// Total number of achievements
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    /// <summary>
    /// Highlighted achievements
    /// </summary>
    [JsonPropertyName("highlighted")]
    public List<StoreAchievement>? Highlighted { get; set; }
}

/// <summary>
/// Store achievement
/// </summary>
public class StoreAchievement
{
    /// <summary>
    /// Achievement name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Achievement path
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}

/// <summary>
/// Store release date
/// </summary>
public class StoreReleaseDate
{
    /// <summary>
    /// Whether the app is coming soon
    /// </summary>
    [JsonPropertyName("coming_soon")]
    public bool ComingSoon { get; set; }
    
    /// <summary>
    /// Release date string
    /// </summary>
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;
}

/// <summary>
/// Store support info
/// </summary>
public class StoreSupportInfo
{
    /// <summary>
    /// Support URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// Support email
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Store content descriptors
/// </summary>
public class StoreContentDescriptors
{
    /// <summary>
    /// Content descriptor IDs
    /// </summary>
    [JsonPropertyName("ids")]
    public List<int>? Ids { get; set; }
    
    /// <summary>
    /// Content descriptor notes
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}

/// <summary>
/// Store rating
/// </summary>
public class StoreRating
{
    /// <summary>
    /// Rating generated
    /// </summary>
    [JsonPropertyName("rating_generated")]
    public string? RatingGenerated { get; set; }
    
    /// <summary>
    /// Rating value
    /// </summary>
    [JsonPropertyName("rating")]
    public string Rating { get; set; } = string.Empty;
    
    /// <summary>
    /// Required age
    /// </summary>
    [JsonPropertyName("required_age")]
    public string RequiredAge { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the app is banned
    /// </summary>
    [JsonPropertyName("banned")]
    public string? Banned { get; set; }
    
    /// <summary>
    /// Whether to use age gate
    /// </summary>
    [JsonPropertyName("use_age_gate")]
    public string? UseAgeGate { get; set; }
    
    /// <summary>
    /// Descriptors
    /// </summary>
    [JsonPropertyName("descriptors")]
    public string? Descriptors { get; set; }
} 