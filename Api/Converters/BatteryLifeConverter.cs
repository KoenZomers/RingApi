using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Converters;

/// <summary>
/// Converts JSON to from a numeric of string value to a numeric value
/// </summary>
public class BatteryLifeConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return reader.GetInt32();
            case JsonTokenType.String:
            {
                var stringValue = reader.GetString();
                if (int.TryParse(stringValue, out var result)) return result;
                return null;
            }
            default:
                return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        writer.WriteNullValue();
    }

    public override bool CanConvert(Type typeToConvert) => true;
}