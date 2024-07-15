namespace GitHubStats.Core;

using GitHubStats.Client;
using GitHubStats.Core.Model;

/// <summary>
/// Provides functionality for retrieving data from GitHub.
/// </summary>
public class GitHubService
{
    /// <summary>
    /// Gets contributor statistics from a GitHub repository.
    /// </summary>
    /// <param name="owner">The user or organization that owns the repository.</param>
    /// <param name="repository">The name of the repository.</param>
    /// <param name="token">The repository access token, if required.</param>
    /// <returns>The repository contributor statistics.</returns>
    /// <remarks>
    /// Initial requests for statistics may require additional time for GitHub to process.
    /// </remarks>
    public async Task<RepositoryStatistics> GetContributorStatistics(string owner, string repository, string? token = null)
    {
        GitHubClient gitHubClient = new();
        ICollection<Client.Model.ContributorStatistics>? gitHubStats = await gitHubClient.GetContributorStatistics(owner, repository, token);

        RepositoryStatistics repoStats = new();
        foreach (Client.Model.ContributorStatistics contributor in gitHubStats)
        {
            ContributorStatistics authorStats = new()
            {
                Username = contributor.Author.Login,
                TotalCommits = contributor.Weeks.Sum(week => week.Commits),
                LinesAdded = contributor.Weeks.Sum(week => week.Additions),
                LinesDeleted = contributor.Weeks.Sum(week => week.Deletions),
            };

            repoStats.Contributors.Add(authorStats);
        }

        return repoStats;
    }
}
