namespace GitHubStats.Commands;

using System;
using GitHubStats.Properties;

/// <summary>
/// Command to display the application help.
/// </summary>
/// <seealso cref="System.Windows.Input.ICommand" />
internal class HelpCommand : ICommand
{
    /// <inheritdoc/>
    public async Task Execute()
    {
        await Console.Out.WriteLineAsync(Resources.ProgramHelp);
    }
}
