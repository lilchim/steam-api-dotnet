using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamApi.Models.Steam.Store;

/// <summary>
/// Custom JSON converter for StoreRequirements that handles both objects and empty arrays
/// The Steam API returns an empty array if the app is not available on a platform
/// </summary>
public class StoreRequirementsConverter : JsonConverter<StoreRequirements?>
{
    public override StoreRequirements? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonTokenType.StartArray)
        {
            // Skip the array (empty or otherwise)
            reader.Skip();
            return null;
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            // Deserialize as normal StoreRequirements object
            return JsonSerializer.Deserialize<StoreRequirements>(ref reader, options);
        }

        // Skip any other token types
        reader.Skip();
        return null;
    }

    public override void Write(Utf8JsonWriter writer, StoreRequirements? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}

/// <summary>
/// Custom JSON converter for required_age that handles both string and integer values
/// The Steam API can return required_age as either a string or integer
/// </summary>
public class RequiredAgeConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (int.TryParse(stringValue, out int result))
            {
                return result;
            }
            return 0; // Default to 0 if parsing fails
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }

        // Return 0 for any other token types
        reader.Skip();
        return 0;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
} 