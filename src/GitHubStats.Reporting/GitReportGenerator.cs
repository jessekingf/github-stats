namespace GitHubStats.Reporting;

using GitHubStats.Model;

/// <summary>
/// Generates Git statistic reports.
/// </summary>
/// <seealso cref="GitHubStats.Reporting.IGitReportGenerator" />
public class GitReportGenerator : IGitReportGenerator
{
    private readonly IGitReportFormatter formatter;
    private readonly IGitReportWriter writer;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitReportGenerator"/> class.
    /// </summary>
    /// <param name="formatter">The report formatter.</param>
    /// <param name="writer">The report writer.</param>
    public GitReportGenerator(IGitReportFormatter formatter, IGitReportWriter writer)
    {
        this.formatter = formatter;
        this.writer = writer;
    }

    /// <inheritdoc/>
    public void GenerateReport(RepositoryStatistics stats)
    {
        string reportData = this.formatter.FormatReport(stats);
        this.writer.WriteReport(reportData);
    }
}
