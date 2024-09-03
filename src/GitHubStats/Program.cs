namespace GitHubStats;

using System.Text;
using System.Threading.Tasks;
using GitHubStats.Core;
using GitHubStats.Model;
using GitHubStats.Reporting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// The entry class of the application.
/// </summary>
internal class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The asynchronous operation.</returns>
    public static async Task Main(string[] args)
    {
        if (args.Length < 2 || args.Length > 3)
        {
            await Console.Error.WriteLineAsync("Invalid arguments.");
            Environment.Exit(1);
        }

        string owner = args[0];
        string repository = args[1];

        string? token = null;
        if (args.Length == 3)
        {
            token = args[2];
        }

        using IHost host = Startup.CreateHost(args);

        IGitService service = host.Services.GetRequiredService<IGitService>();
        RepositoryStatistics stats = await service.GetContributorStatistics(owner, repository, token);

        IGitReportGenerator reportGenerator = host.Services.GetRequiredService<IGitReportGenerator>();
        reportGenerator.GenerateReport(stats);
    }
}
