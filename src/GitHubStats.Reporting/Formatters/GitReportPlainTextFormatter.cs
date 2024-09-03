namespace GitHubStats.Reporting.Formatters;

using System.Text;
using GitHubStats.Model;

/// <summary>
/// Formats Git statistic reports for plain-text output.
/// </summary>
/// <seealso cref="GitHubStats.Reporting.IGitReportFormatter" />
public class GitReportPlainTextFormatter : IGitReportFormatter
{
    /// <summary>
    /// Formats the Git report in plain-text.
    /// </summary>
    /// <param name="stats">The git repository statistics to report on.</param>
    /// <returns>The formatted report data.</returns>
    public string FormatReport(RepositoryStatistics stats)
    {
        ArgumentNullException.ThrowIfNull(stats, nameof(stats));

        StringBuilder report = new();
        foreach (ContributorStatistics contributor in stats.Contributors)
        {
            report.AppendLine(contributor.Username);
            report.AppendLine($"Commits: {contributor.TotalCommits}");
            report.AppendLine($"Lines added: {contributor.LinesAdded}");
            report.AppendLine($"Lines deleted: {contributor.LinesDeleted}");
            report.AppendLine($"Lines changed: {contributor.LinesChanged}");
            report.AppendLine();
        }

        return report.ToString();
    }
}
