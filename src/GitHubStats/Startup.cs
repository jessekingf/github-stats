namespace GitHubStats;

using System.Net.Http.Headers;
using GitHub.Client;
using GitHubStats.Core.Git;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Configures the application on startup.
/// </summary>
public class Startup
{
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
        services.AddHttpClient<IGitHubClient, GitHubClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            client.DefaultRequestHeaders.Add("User-Agent", "GitHub Statistics");

            // TODO: Config app name
        });

        services.AddTransient<IGitService, GitHubService>();
    }
}
