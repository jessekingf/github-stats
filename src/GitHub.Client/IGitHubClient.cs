namespace GitHub.Client;

using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.Client.Model;

/// <summary>
/// Provides access to GitHub.
/// </summary>
public interface IGitHubClient
{
    /// <summary>
    /// Gets contributor statistics from a GitHub repository.
    /// </summary>
    /// <param name="owner">The user or organization that owns the repository.</param>
    /// <param name="repository">The name of the repository.</param>
    /// <param name="token">The repository access token, if required.</param>
    /// <returns>The repository contributor statistics.</returns>
    Task<ICollection<ContributorStatistics>> GetContributorStatistics(string owner, string repository, string? token = null);
}
