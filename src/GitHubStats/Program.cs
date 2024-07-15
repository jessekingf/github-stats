﻿namespace GitHubStats;

using System.Text;
using System.Threading.Tasks;
using GitHubStats.Core;
using GitHubStats.Core.Model;

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

        Console.WriteLine("Retrieving GitHub statistics...");

        GitHubService service = new();
        RepositoryStatistics repoStats = await service.GetContributorStatistics(owner, repository, token);
        PrintStats(repoStats);
    }

    private static void PrintStats(RepositoryStatistics repoStats)
    {
        StringBuilder output = new();
        foreach (ContributorStatistics contributor in repoStats.Contributors)
        {
            output.AppendLine(contributor.Username);
            output.AppendLine($"Commits: {contributor.TotalCommits}");
            output.AppendLine($"Lines added: {contributor.LinesAdded}");
            output.AppendLine($"Lines deleted: {contributor.LinesDeleted}");
            output.AppendLine($"Lines changed: {contributor.LinesChanged}");
            output.AppendLine();
        }

        Console.Clear();
        Console.WriteLine(output);
    }
}