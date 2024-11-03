namespace GitHubStats.Core.Services;

using System.Threading.Tasks;
using GitHubStats.Model;

/// <summary>
/// Provides functionality for retrieving data from Git.
/// </summary>
public interface IGitService
{
    /// <summary>
    /// Gets contributor statistics from a Git repository.
    /// </summary>
    /// <param name="owner">The user or organization that owns the repository.</param>
    /// <param name="repository">The name of the repository.</param>
    /// <param name="token">The repository access token, if required.</param>
    /// <returns>The repository contributor statistics.</returns>
    Task<RepositoryStatistics> GetContributorStatistics(string owner, string repository, string? token = null);
}
