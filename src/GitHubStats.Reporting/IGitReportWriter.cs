namespace GitHubStats.Reporting;

/// <summary>
/// Provides functionality for writing out Git reports.
/// </summary>
public interface IGitReportWriter
{
    /// <summary>
    /// Writes the report.
    /// </summary>
    /// <param name="reportData">The report data to output.</param>
    void WriteReport(string reportData);
}
