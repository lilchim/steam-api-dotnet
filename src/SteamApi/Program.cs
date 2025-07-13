using SteamApi.Configuration;
using SteamApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Steam API options
builder.Services.Configure<SteamApiOptions>(
    builder.Configuration.GetSection(SteamApiOptions.SectionName));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
