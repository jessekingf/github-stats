namespace GitHubStats.Client.Model;

using System.Text.Json.Serialization;
using GitHub.Client.Model;

/// <summary>
/// Represents GitHub contributor statistics for a repository.
/// </summary>
public record ContributorStatistics
{
    /// <summary>
    /// Gets the author information.
    /// </summary>
    [JsonPropertyName("author")]
    public required Author Author { get; init; }

    /// <summary>
    /// Gets the total number of commits.
    /// </summary>
    [JsonPropertyName("total")]
    public int TotalCommits { get; init; }

    /// <summary>
    /// Gets the statistics grouped by week.
    /// </summary>
    [JsonPropertyName("weeks")]
    public required ICollection<WeekStatistics> Weeks { get; init; }
}
