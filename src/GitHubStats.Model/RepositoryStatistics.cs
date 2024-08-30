namespace GitHubStats.Model;

/// <summary>
/// Statistics for a repository.
/// </summary>
public record RepositoryStatistics
{
    /// <summary>
    /// Gets the statistics by contributor.
    /// </summary>
    public IList<ContributorStatistics> Contributors { get; init; } = new List<ContributorStatistics>();
}
