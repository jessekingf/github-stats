﻿namespace GitHubStats.Commands;

using GitHubStats.Core.Services;
using GitHubStats.Model;
using GitHubStats.Reporting;

/// <summary>
/// Command to generate the GitHub statistics reports.
/// </summary>
/// <seealso cref="GitHubStats.Commands.ICommand" />
public class StatisticsReportCommand : ICommand
{
    private readonly IGitService gitService;
    private readonly IGitReportGenerator gitReportGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsReportCommand"/> class.
    /// </summary>
    /// /// <param name="gitService">The Git service.</param>
    /// <param name="gitReportGenerator">The report generator.</param>
    public StatisticsReportCommand(IGitService gitService, IGitReportGenerator gitReportGenerator)
    {
        this.gitService = gitService;
        this.gitReportGenerator = gitReportGenerator;
    }

    /// <summary>
    /// Gets or sets the repository name.
    /// </summary>
    public required string Repository
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

    /// <inheritdoc/>
    public async Task Execute()
    {
        RepositoryStatistics stats = await this.gitService.GetContributorStatistics(this.Owner, this.Repository, this.Token);
        this.gitReportGenerator.GenerateReport(stats);
    }
}