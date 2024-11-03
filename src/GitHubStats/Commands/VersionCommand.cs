namespace GitHubStats.Commands;

using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Command to display the application version.
/// </summary>
/// <seealso cref="System.Windows.Input.ICommand" />
internal class VersionCommand : ICommand
{
    /// <inheritdoc/>
    public async Task Execute()
    {
        Version? version = Assembly.GetEntryAssembly()?.GetName()?.Version;
        if (version == null)
        {
            throw new InvalidOperationException("The application version was not found.");
        }

        await Console.Out.WriteLineAsync(version.ToString());
    }
}
