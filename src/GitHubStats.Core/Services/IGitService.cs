namespace GitHubStats.Core.Services;

using System.Threading.Tasks;
using GitHubStats.Model;

/// <summary>
/// Provides functionality for retrieving data from Git.
/// </summary>
public interface IGitService
{
    /// <summary>
    /// Gets contributor statistics for a Git repository.
    /// </summary>
    /// <param name="repository">The Git repository details.</param>
    /// <returns>The repository contributor statistics.</returns>
    Task<RepositoryStatistics> GetContributorStatistics(Repository repository);
}
