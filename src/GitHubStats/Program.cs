namespace GitHubStats;

using System.Threading.Tasks;
using GitHubStats.Commands;
using GitHubStats.Exceptions;
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
        using IHost host = Startup.CreateHost(args);

        try
        {
            ICommand command = CommandParser.Parse(host, args);
            await command.Execute();
        }
        catch (InvalidOptionException ex)
        {
            await Console.Error.WriteLineAsync(ex.Message);
            await new HelpCommand().Execute();
            Environment.Exit(1);
        }

        Environment.Exit(0);
    }
}
