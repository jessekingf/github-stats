namespace GitHubStats.Client;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using GitHubStats.Client.Model;

/// <summary>
/// Provides access to GitHub via the public API.
/// </summary>
public class GitHubClient
{
    private static readonly Uri GitHubApiUrl = new(@"https://api.github.com/");

    /// <summary>
    /// Gets or sets the maximum number of attempts to make to get repository statistics.
    /// </summary>
    public int MaxStatAttempts { get; set; } = 60;

    /// <summary>
    /// Gets or sets the delay in milliseconds between web request attempts.
    /// </summary>
    public int RetryDelay { get; set; } = 2000;

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
    public async Task<ICollection<ContributorStatistics>> GetContributorStatistics(string owner, string repository, string? token = null)
    {
        Uri apiUrl = new(GitHubApiUrl, $"repos/{owner}/{repository}/stats/contributors");
        using HttpClient client = new HttpClient();

        // Set the request headers.
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        client.DefaultRequestHeaders.Add("User-Agent", "GitHub Statistics");

        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        // Make the request to GitHub.
        // GitHub will return 202 (accepted) while is is processing the numbers.
        // Retry until the statistics are returned.
        List<ContributorStatistics>? stats = null;
        int attempts = 0;
        while (++attempts <= this.MaxStatAttempts && stats == null)
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                if (attempts >= this.MaxStatAttempts)
                {
                    throw new HttpRequestException($"Request did not succeed after {this.MaxStatAttempts} retries.");
                }

                // Statistics are still being calculated by GitHub, wait and try again.
                await Task.Delay(this.RetryDelay);
                continue;
            }

            // Parse the response.
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            stats = JsonSerializer.Deserialize<List<ContributorStatistics>>(responseData);
        }

        return stats ?? new List<ContributorStatistics>();
    }
}
