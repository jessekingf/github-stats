namespace GitHubStats.Reporting.Writers;

/// <summary>
/// Outputs Git statistics reports to the console.
/// </summary>
/// <seealso cref="GitHubStats.Reporting.IGitReportWriter" />
public class GitReportConsoleWriter : IGitReportWriter
{
    /// <summary>
    /// Outputs the Git report to the console.
    /// </summary>
    /// <param name="reportData">The report data to output.</param>
    public void WriteReport(string reportData)
    {
        ArgumentException.ThrowIfNullOrEmpty(reportData, nameof(reportData));

        Console.WriteLine(reportData);
    }
}
