using System.Reflection;
using SteamApi.Configuration;
using SteamApi.Middleware;
using SteamApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Steam API", Version = "v1" });
    
    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Add API key parameter to Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "X-API-Key",
        Description = "API key for authentication"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure Steam API options
builder.Services.Configure<SteamApiOptions>(
    builder.Configuration.GetSection(SteamApiOptions.SectionName));

// Configure API Key options
builder.Services.Configure<ApiKeyOptions>(
    builder.Configuration.GetSection(ApiKeyOptions.SectionName));

// Validate Steam API configuration
var steamApiOptions = builder.Configuration.GetSection(SteamApiOptions.SectionName).Get<SteamApiOptions>();
if (string.IsNullOrEmpty(steamApiOptions?.ApiKey))
{
    throw new InvalidOperationException(
        "Steam API Key is required. Set it using User Secrets (development) or Environment Variables (production). " +
        "For development: dotnet user-secrets set \"SteamApi:ApiKey\" \"your-steam-api-key\"");
}

// Register Steam API services
builder.Services.AddHttpClient<ISteamApiService, SteamApiService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(steamApiOptions.TimeoutSeconds);
    client.DefaultRequestHeaders.Add("User-Agent", "SteamApi-DotNet/1.0");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add API key middleware
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
