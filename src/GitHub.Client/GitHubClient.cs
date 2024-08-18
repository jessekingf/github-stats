namespace GitHub.Client;

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using GitHub.Client.Model;

/// <summary>
/// Provides access to GitHub via the public API.
/// </summary>
public class GitHubClient : IGitHubClient
{
    private readonly HttpClient httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client for making requests to the GitHub API.</param>
    public GitHubClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

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
        if (!string.IsNullOrEmpty(token))
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // Make the request to GitHub.
        // GitHub will return 202 (accepted) while is is processing the numbers.
        // Retry until the statistics are returned.
        Uri apiUrl = new($"repos/{owner}/{repository}/stats/contributors", UriKind.Relative);
        List<ContributorStatistics>? stats = null;
        int attempts = 0;

        while (++attempts <= this.MaxStatAttempts && stats == null)
        {
            HttpResponseMessage response = await this.httpClient.GetAsync(apiUrl);

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
