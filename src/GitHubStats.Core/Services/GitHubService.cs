namespace GitHubStats.Core.Services;

using System.Collections.Generic;
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
    /// <param name="repository">The Git repository details.</param>
    /// <returns>The repository contributor statistics.</returns>
    /// <remarks>
    /// Initial requests for statistics may require additional time for GitHub to process.
    /// </remarks>
    public async Task<RepositoryStatistics> GetContributorStatistics(Repository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);

        ICollection<GitHub.Client.Model.ContributorStatistics>? gitHubStats =
            await this.gitHubClient.GetContributorStatistics(repository.Owner, repository.Name, repository.Token);

        List<ContributorStatistics> contributors = [];
        foreach (GitHub.Client.Model.ContributorStatistics contributor in gitHubStats)
        {
            ContributorStatistics authorStats = new()
            {
                Username = contributor.Author.Login,
                TotalCommits = contributor.Weeks.Sum(week => week.Commits),
                LinesAdded = contributor.Weeks.Sum(week => week.Additions),
                LinesDeleted = contributor.Weeks.Sum(week => week.Deletions),
            };

            contributors.Add(authorStats);
        }

        RepositoryStatistics repoStats = new()
        {
            Repository = repository,
            Contributors = contributors,
        };

        return repoStats;
    }
}
