namespace GitHub.Client.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents information about the GitHub author.
/// </summary>
public record Author
{
    /// <summary>
    /// Gets the login name of the author.
    /// </summary>
    [JsonPropertyName("login")]
    public required string Login { get; init; }

    /// <summary>
    /// Gets the unique identifier of the author.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// Gets the base64-encoded node ID of the author, which uniquely identifies the resource
    /// within GitHub's system. This ID is used internally by GitHub's GraphQL API to reference
    /// the author.
    /// </summary>
    [JsonPropertyName("node_id")]
    public string? NodeId { get; init; }

    /// <summary>
    /// Gets the URL of the author's avatar image.
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public Uri? AvatarUrl { get; init; }

    /// <summary>
    /// Gets the Gravatar ID of the author, which is a globally unique identifier for the user's
    /// Gravatar profile image.
    /// </summary>
    [JsonPropertyName("gravatar_id")]
    public Uri? GravatarId { get; init; }

    /// <summary>
    /// Gets the API URL of the author.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; init; }

    /// <summary>
    /// Gets the HTML URL of the author's GitHub profile.
    /// </summary>
    [JsonPropertyName("html_url")]
    public Uri? HtmlUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the author's followers.
    /// </summary>
    [JsonPropertyName("followers_url")]
    public Uri? FollowersUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the users that the author is following.
    /// </summary>
    [JsonPropertyName("following_url")]
    public Uri? FollowingUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the author's gists.
    /// </summary>
    [JsonPropertyName("gists_url")]
    public Uri? GistsUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the repositories that the author has starred.
    /// </summary>
    [JsonPropertyName("starred_url")]
    public Uri? StarredUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the author's subscriptions.
    /// </summary>
    [JsonPropertyName("subscriptions_url")]
    public Uri? SubscriptionsUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the organizations that the author belongs to.
    /// </summary>
    [JsonPropertyName("organizations_url")]
    public Uri? OrganizationsUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the author's repositories.
    /// </summary>
    [JsonPropertyName("repos_url")]
    public Uri? ReposUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the events performed by the author.
    /// </summary>
    [JsonPropertyName("events_url")]
    public Uri? EventsUrl { get; init; }

    /// <summary>
    /// Gets the API URL for the events received by the author.
    /// </summary>
    [JsonPropertyName("received_events_url")]
    public Uri? ReceivedEventsUrl { get; init; }

    /// <summary>
    /// Gets the type of the author (e.g., "User" or "Organization").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Gets a value indicating whether the author is a site administrator.
    /// </summary>
    [JsonPropertyName("site_admin")]
    public bool SiteAdmin { get; init; }
}
