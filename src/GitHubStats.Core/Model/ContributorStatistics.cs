namespace GitHubStats.Core.Model;

/// <summary>
/// Git statistics for a contributor.
/// </summary>
public record ContributorStatistics
{
    /// <summary>
    /// Gets the contributor's username.
    /// </summary>
    public string? Username
    {
        get;
        init;
    }

    /// <summary>
    /// Gets the total commits by the contributor.
    /// </summary>
    public int TotalCommits
    {
        get;
        init;
    }

    /// <summary>
    /// Gets the total number of lines of code added by the contributor.
    /// </summary>
    public int LinesAdded
    {
        get;
        init;
    }

    /// <summary>
    /// Gets the total number of lines of code removed by the contributor.
    /// </summary>
    public int LinesDeleted
    {
        get;
        init;
    }

    /// <summary>
    /// Gets the total number of lines of code changed by a contributor.
    /// </summary>
    /// <remarks>
    /// The sum of the number of lines added and deleted.
    /// </remarks>
    public int LinesChanged
    {
        get
        {
            return this.LinesAdded + this.LinesDeleted;
        }
    }
}
