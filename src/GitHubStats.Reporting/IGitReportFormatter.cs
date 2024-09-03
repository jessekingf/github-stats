namespace GitHubStats.Reporting;

using GitHubStats.Model;

/// <summary>
/// Provides functionality for formatting Git statistics reports.
/// </summary>
public interface IGitReportFormatter
{
    /// <summary>
    /// Formats the Git report.
    /// </summary>
    /// <param name="stats">The git repository statistics to report on.</param>
    /// <returns>The formatted report data.</returns>
    string FormatReport(RepositoryStatistics stats);
}
