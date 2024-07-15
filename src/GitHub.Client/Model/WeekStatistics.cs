namespace GitHub.Client.Model;

using System.Text.Json.Serialization;
using Common.Json;

/// <summary>
/// The GitHub statistics for a given week.
/// </summary>
public record WeekStatistics
{
    /// <summary>
    /// Gets the week number.
    /// </summary>
    [JsonPropertyName("w")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the number of additions.
    /// </summary>
    [JsonPropertyName("a")]
    public int Additions { get; init; }

    /// <summary>
    /// Gets the number of deletions.
    /// </summary>
    [JsonPropertyName("d")]
    public int Deletions { get; init; }

    /// <summary>
    /// Gets the number of commits.
    /// </summary>
    [JsonPropertyName("c")]
    public int Commits { get; init; }
}
