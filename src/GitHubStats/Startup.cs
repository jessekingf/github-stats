﻿namespace GitHubStats;

using System.Net.Http.Headers;
using System.Reflection;
using GitHub.Client;
using GitHubStats.Commands;
using GitHubStats.Core.Services;
using GitHubStats.Reporting;
using GitHubStats.Reporting.Formatters;
using GitHubStats.Reporting.Writers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Configures the application on startup.
/// </summary>
public class Startup
{
    private const string DefaultAppName = "GitHub Statistics";

    /// <summary>
    /// Creates the application host.
    /// </summary>
    /// <param name="args">The application arguments.</param>
    /// <returns>The application host.</returns>
    public static IHost CreateHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                logging.AddFilter((provider, category, logLevel) =>
                {
                    return !Console.IsOutputRedirected;
                });
            })
            .ConfigureServices((context, services) =>
            {
                Startup startup = new();
                startup.ConfigureServices(services);
            })
            .Build();
    }

    /// <summary>
    /// Configures the application services.
    /// </summary>
    /// <param name="services">The application service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureCommands(services);

        services.AddHttpClient<IGitHubClient, GitHubClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            client.DefaultRequestHeaders.Add("User-Agent", GetAppName());
        });

        services.AddTransient<IGitService, GitHubService>();
        services.AddTransient<IGitReportFormatter, GitReportPlainTextFormatter>();
        services.AddTransient<IGitReportGenerator, GitReportGenerator>();
        services.AddTransient<IGitReportWriter, GitReportConsoleWriter>();
    }

    private static void ConfigureCommands(IServiceCollection services)
    {
        services.AddTransient<HelpCommand>();
        services.AddTransient<StatisticsReportCommand>();
        services.AddTransient<VersionCommand>();
    }

    private static string GetAppName()
    {
        return Assembly.GetEntryAssembly()
            ?.GetCustomAttribute<AssemblyProductAttribute>()
            ?.Product
            ?? DefaultAppName;
    }
}
