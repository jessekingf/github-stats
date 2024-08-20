namespace GitHub.Client.Model;

using System.Text.Json.Serialization;

/// <summary>
/// The type of GitHub user.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    /// <summary>
    /// A standard user.
    /// </summary>
    User,

    /// <summary>
    /// An organization user.
    /// </summary>
    Organization,
}
