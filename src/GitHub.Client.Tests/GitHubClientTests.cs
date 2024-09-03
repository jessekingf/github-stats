namespace GitHub.Client.Tests;

using System.Net;
using System.Net.Http;
using GitHub.Client.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

/// <summary>
/// A test fixture for the <see cref="GitHubClient"/> class.
/// </summary>
[TestClass]
public class GitHubClientTests
{
    /// <summary>
    /// A good case constructor test.
    /// </summary>
    [TestMethod]
    public void Ctor_ValidHttpClient_Success()
    {
        Mock<HttpClient> httpClientMock = new();
        Assert.IsNotNull(httpClientMock.Object);
    }

    /// <summary>
    /// A good case test for GetContributorStatistics where the GitHub API
    /// returns a valid response (200) on the first request.
    /// </summary>
    /// <returns>The asynchronous test operation.</returns>
    [TestMethod]
    public async Task GetContributorStatistics_ValidApiResponse_Success()
    {
        // Arrange
        Mock<HttpMessageHandler> httpMessageHandlerMock = new();
        using HttpClient httpClient = new(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.github.com/"),
        };
        GitHubClient gitHubClient = new(httpClient);

        string owner = "octocat";
        string repository = "Hello-World";
        string expectedUri = $"https://api.github.com/repos/{owner}/{repository}/stats/contributors";

        string responseContent = @"
        [
          {
            ""author"": {
              ""login"": ""octocat"",
              ""id"": 1,
              ""node_id"": ""MDQ6VXNlcjE="",
              ""avatar_url"": ""https://github.com/images/error/octocat_happy.gif"",
              ""gravatar_id"": ""test"",
              ""url"": ""https://api.github.com/users/octocat"",
              ""html_url"": ""https://github.com/octocat"",
              ""followers_url"": ""https://api.github.com/users/octocat/followers"",
              ""following_url"": ""https://api.github.com/users/octocat/following{/other_user}"",
              ""gists_url"": ""https://api.github.com/users/octocat/gists{/gist_id}"",
              ""starred_url"": ""https://api.github.com/users/octocat/starred{/owner}{/repo}"",
              ""subscriptions_url"": ""https://api.github.com/users/octocat/subscriptions"",
              ""organizations_url"": ""https://api.github.com/users/octocat/orgs"",
              ""repos_url"": ""https://api.github.com/users/octocat/repos"",
              ""events_url"": ""https://api.github.com/users/octocat/events{/privacy}"",
              ""received_events_url"": ""https://api.github.com/users/octocat/received_events"",
              ""type"": ""User"",
              ""site_admin"": false
            },
            ""total"": 15,
            ""weeks"": [
              {
                ""w"": 1367712000,
                ""a"": 6898,
                ""d"": 77,
                ""c"": 10
              },
              {
                ""w"": 1368316800,
                ""a"": 5000,
                ""d"": 50,
                ""c"": 5
              }
            ]
          }
        ]";

        List<ContributorStatistics> expected =
        [
            new ContributorStatistics
            {
                Author = new Author
                {
                    Login = "octocat",
                    Id = 1,
                    NodeId = "MDQ6VXNlcjE=",
                    AvatarUrl = "https://github.com/images/error/octocat_happy.gif",
                    GravatarId = "test",
                    Url = "https://api.github.com/users/octocat",
                    HtmlUrl = "https://github.com/octocat",
                    FollowersUrl = "https://api.github.com/users/octocat/followers",
                    FollowingUrl = "https://api.github.com/users/octocat/following{/other_user}",
                    GistsUrl = "https://api.github.com/users/octocat/gists{/gist_id}",
                    StarredUrl = "https://api.github.com/users/octocat/starred{/owner}{/repo}",
                    SubscriptionsUrl = "https://api.github.com/users/octocat/subscriptions",
                    OrganizationsUrl = "https://api.github.com/users/octocat/orgs",
                    ReposUrl = "https://api.github.com/users/octocat/repos",
                    EventsUrl = "https://api.github.com/users/octocat/events{/privacy}",
                    ReceivedEventsUrl = "https://api.github.com/users/octocat/received_events",
                    Type = UserType.User,
                    SiteAdmin = false,
                },
                TotalCommits = 15,
                Weeks = new List<WeekStatistics>
                {
                    new WeekStatistics
                    {
                        StartDate = new DateTime(2013, 5, 5),
                        Additions = 6898,
                        Deletions = 77,
                        Commits = 10,
                    },
                    new WeekStatistics
                    {
                        StartDate = new DateTime(2013, 5, 12),
                        Additions = 5000,
                        Deletions = 50,
                        Commits = 5,
                    },
                },
            },
        ];

        using HttpResponseMessage responseMessage = new(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent),
        };

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri != null && req.RequestUri.ToString() == expectedUri),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        ICollection<ContributorStatistics> result = await gitHubClient.GetContributorStatistics(owner, repository);

        // Assert
        this.AssertAreEqual(expected, result.ToList());
    }

    /// <summary>
    /// A good case test for GetContributorStatistics where the GitHub API
    /// returns a valid accept response (202) on the first request and then the
    /// payload response (200) on the second attempt.
    /// </summary>
    /// <returns>The asynchronous test operation.</returns>
    [TestMethod]
    public async Task GetContributorStatistics_AcceptedThenSuccess_Success()
    {
        // Arrange
        Mock<HttpMessageHandler> httpMessageHandlerMock = new();
        using HttpClient httpClient = new(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.github.com/"),
        };
        GitHubClient gitHubClient = new(httpClient);

        string owner = "octocat";
        string repository = "Hello-World";
        string expectedUri = $"https://api.github.com/repos/{owner}/{repository}/stats/contributors";

        string responseContent = @"
        [
          {
            ""author"": {
              ""login"": ""octocat"",
              ""id"": 1,
              ""node_id"": ""MDQ6VXNlcjE="",
              ""avatar_url"": ""https://github.com/images/error/octocat_happy.gif"",
              ""gravatar_id"": ""test"",
              ""url"": ""https://api.github.com/users/octocat"",
              ""html_url"": ""https://github.com/octocat"",
              ""followers_url"": ""https://api.github.com/users/octocat/followers"",
              ""following_url"": ""https://api.github.com/users/octocat/following{/other_user}"",
              ""gists_url"": ""https://api.github.com/users/octocat/gists{/gist_id}"",
              ""starred_url"": ""https://api.github.com/users/octocat/starred{/owner}{/repo}"",
              ""subscriptions_url"": ""https://api.github.com/users/octocat/subscriptions"",
              ""organizations_url"": ""https://api.github.com/users/octocat/orgs"",
              ""repos_url"": ""https://api.github.com/users/octocat/repos"",
              ""events_url"": ""https://api.github.com/users/octocat/events{/privacy}"",
              ""received_events_url"": ""https://api.github.com/users/octocat/received_events"",
              ""type"": ""User"",
              ""site_admin"": false
            },
            ""total"": 135,
            ""weeks"": [
              {
                ""w"": 1367712000,
                ""a"": 6898,
                ""d"": 77,
                ""c"": 10
              }
            ]
          }
        ]";

        List<ContributorStatistics> expected = new()
        {
            new ContributorStatistics
            {
                Author = new Author
                {
                    Login = "octocat",
                    Id = 1,
                    NodeId = "MDQ6VXNlcjE=",
                    AvatarUrl = "https://github.com/images/error/octocat_happy.gif",
                    GravatarId = "test",
                    Url = "https://api.github.com/users/octocat",
                    HtmlUrl = "https://github.com/octocat",
                    FollowersUrl = "https://api.github.com/users/octocat/followers",
                    FollowingUrl = "https://api.github.com/users/octocat/following{/other_user}",
                    GistsUrl = "https://api.github.com/users/octocat/gists{/gist_id}",
                    StarredUrl = "https://api.github.com/users/octocat/starred{/owner}{/repo}",
                    SubscriptionsUrl = "https://api.github.com/users/octocat/subscriptions",
                    OrganizationsUrl = "https://api.github.com/users/octocat/orgs",
                    ReposUrl = "https://api.github.com/users/octocat/repos",
                    EventsUrl = "https://api.github.com/users/octocat/events{/privacy}",
                    ReceivedEventsUrl = "https://api.github.com/users/octocat/received_events",
                    Type = UserType.User,
                    SiteAdmin = false,
                },
                TotalCommits = 135,
                Weeks = new List<WeekStatistics>
                {
                    new WeekStatistics
                    {
                        StartDate = new DateTime(2013, 5, 5),
                        Additions = 6898,
                        Deletions = 77,
                        Commits = 10,
                    },
                },
            },
        };

        using HttpResponseMessage acceptedResponse = new(HttpStatusCode.Accepted);
        using HttpResponseMessage successResponse = new(HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent),
        };

        httpMessageHandlerMock
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri != null && req.RequestUri.ToString() == expectedUri),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(acceptedResponse)
            .ReturnsAsync(successResponse);

        // Act
        ICollection<ContributorStatistics> result = await gitHubClient.GetContributorStatistics(owner, repository);

        // Assert
        this.AssertAreEqual(expected, result.ToList());
    }

    /// <summary>
    /// A test for GetContributorStatistics where the GitHub API returns
    /// an accept response (202) repeatedly until the limit is reached.
    /// </summary>
    /// <returns>The asynchronous test operation.</returns>
    [TestMethod]
    public async Task GetContributorStatistics_MaxAttemptsHit_ThrowsHttpRequestException()
    {
        // Arrange
        Mock<HttpMessageHandler> httpMessageHandlerMock = new();
        using HttpClient httpClient = new(httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.github.com/"),
        };
        GitHubClient gitHubClient = new(httpClient)
        {
            MaxStatAttempts = 3,
            RetryDelay = 1,
        };

        string owner = "octocat";
        string repository = "Hello-World";
        string expectedUri = $"https://api.github.com/repos/{owner}/{repository}/stats/contributors";

        using HttpResponseMessage acceptedResponse = new(HttpStatusCode.Accepted);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri != null && req.RequestUri.ToString() == expectedUri),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(acceptedResponse);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<HttpRequestException>(() => gitHubClient.GetContributorStatistics(owner, repository));
    }

    private void AssertAreEqual(IList<ContributorStatistics> expected, IList<ContributorStatistics> actual)
    {
        Assert.AreEqual(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; ++i)
        {
            ContributorStatistics expectedStats = expected[i];
            ContributorStatistics actualStats = actual[i];

            Assert.AreEqual(expectedStats.Author, actualStats.Author);
            Assert.AreEqual(expectedStats.TotalCommits, actualStats.TotalCommits);
            CollectionAssert.AreEquivalent(expectedStats.Weeks.ToList(), actualStats.Weeks.ToList());
        }
    }
}
