namespace Common.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// A Test.
/// </summary>
/// <seealso cref="JsonConverter{DateTime}" />
public class UnixTimestampConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// Reads and converts the JSON to type DateTime.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    /// <returns>
    /// The converted value.
    /// </returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        long unixTimestamp = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
    }

    /// <summary>
    /// Writes a specified value as JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to convert to JSON.</param>
    /// <param name="options">An object that specifies serialization options to use.</param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);

        long unixTimestamp = new DateTimeOffset(value).ToUnixTimeSeconds();
        writer.WriteNumberValue(unixTimestamp);
    }
}
