namespace GitHubStats;

using GitHubStats.Commands;
using GitHubStats.Exceptions;
using GitHubStats.Properties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Parses the command line arguments.
/// </summary>
public class CommandParser
{
    private readonly IHost host;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandParser"/> class.
    /// </summary>
    /// <param name="host">The application host.</param>
    public CommandParser(IHost host)
    {
        this.host = host;
    }

    /// <summary>
    /// Parses the program options from the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>The program options.</returns>
    public ICommand Parse(string[] args)
    {
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
                        return this.host.Services.GetRequiredService<HelpCommand>();
                    case "--version":
                    case "-v":
                        return this.host.Services.GetRequiredService<VersionCommand>();
                    default:
                        throw new InvalidOptionException(string.Format(Resources.InvalidOption, arg));
                }
            }

            nonSwitchArgs.Add(arg);
        }

        return this.BuildReportCommand(nonSwitchArgs);
    }

    private StatisticsReportCommand BuildReportCommand(IReadOnlyList<string> args)
    {
        if (args.Count < 2)
        {
            throw new InvalidOptionException(Resources.InvalidArguments);
        }

        StatisticsReportCommand reportCommand = this.host.Services.GetRequiredService<StatisticsReportCommand>();

        reportCommand.Owner = args[0];
        reportCommand.Repository = args[1];
        if (args.Count == 3)
        {
            reportCommand.Token = args[2];
        }

        return reportCommand;
    }
}
