namespace GitHubStats.Reporting;

using GitHubStats.Model;

/// <summary>
/// Provides functionality for generating Git reports.
/// </summary>
public interface IGitReportGenerator
{
    /// <summary>
    /// Generates the Git report.
    /// </summary>
    /// <param name="stats">The git repository statistics to report on.</param>
    void GenerateReport(RepositoryStatistics stats);
}
