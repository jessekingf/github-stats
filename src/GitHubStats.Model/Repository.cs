namespace GitHubStats.Model;

/// <summary>
/// Git repository details.
/// </summary>
public record Repository
{
    /// <summary>
    /// Gets or sets the repository name.
    /// </summary>
    public required string Name
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the repository owner.
    /// </summary>
    public required string Owner
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the repository token.
    /// </summary>
    public string? Token
    {
        get;
        set;
    }
}
