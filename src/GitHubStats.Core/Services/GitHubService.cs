namespace GitHubStats.Core.Services;

using GitHub.Client;
using GitHubStats.Model;

/// <summary>
/// Provides functionality for retrieving data from GitHub.
/// </summary>
public class GitHubService : IGitService
{
    private readonly IGitHubClient gitHubClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubService"/> class.
    /// </summary>
    /// <param name="gitHubClient">The git hub client.</param>
    public GitHubService(IGitHubClient gitHubClient)
    {
        this.gitHubClient = gitHubClient;
    }

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
        ICollection<GitHub.Client.Model.ContributorStatistics>? gitHubStats = await this.gitHubClient.GetContributorStatistics(owner, repository, token);

        RepositoryStatistics repoStats = new();
        foreach (GitHub.Client.Model.ContributorStatistics contributor in gitHubStats)
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
