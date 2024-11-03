namespace GitHubStats.Commands;

/// <summary>
/// A program command.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Executes this command.
    /// </summary>
    /// <returns>The asynchronous operation.</returns>
    Task Execute();
}
