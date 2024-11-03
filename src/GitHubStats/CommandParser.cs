namespace GitHubStats;

using GitHubStats.Commands;
using GitHubStats.Exceptions;
using GitHubStats.Properties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Parses the command line arguments.
/// </summary>
public static class CommandParser
{
    /// <summary>
    /// Parses the program options from the command-line arguments.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The program options.</returns>
    public static ICommand Parse(IHost host, string[] args)
    {
        ArgumentNullException.ThrowIfNull(host);

        List<string> nonSwitchArgs = new();

        foreach (string arg in args ?? Array.Empty<string>())
        {
            if (string.IsNullOrEmpty(arg))
            {
                continue;
            }

            if (arg.StartsWith('-'))
            {
                switch (arg.ToLowerInvariant())
                {
                    case "--help":
                    case "-h":
                        return host.Services.GetRequiredService<HelpCommand>();
                    case "--version":
                    case "-v":
                        return host.Services.GetRequiredService<VersionCommand>();
                    default:
                        throw new InvalidOptionException(string.Format(Resources.InvalidOption, arg));
                }
            }

            nonSwitchArgs.Add(arg);
        }

        return BuildReportCommand(host, nonSwitchArgs);
    }

    private static StatisticsReportCommand BuildReportCommand(IHost host, IReadOnlyList<string> args)
    {
        if (args.Count < 2)
        {
            throw new InvalidOptionException(Resources.InvalidArguments);
        }

        StatisticsReportCommand reportCommand = host.Services.GetRequiredService<StatisticsReportCommand>();

        reportCommand.Owner = args[0];
        reportCommand.Repository = args[1];
        if (args.Count == 3)
        {
            reportCommand.Token = args[2];
        }

        return reportCommand;
    }
}
