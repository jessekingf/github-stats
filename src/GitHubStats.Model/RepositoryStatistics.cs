namespace GitHubStats.Model;

/// <summary>
/// Statistics for a repository.
/// </summary>
public record RepositoryStatistics
{
    /// <summary>
    /// Gets the repository details.
    /// </summary>
    public required Repository Repository { get; init; }

    /// <summary>
    /// Gets the statistics by contributor.
    /// </summary>
    public required IReadOnlyList<ContributorStatistics> Contributors { get; init; } = [];
}
