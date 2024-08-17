# GitHub Statistics

A utility for reporting GitHub repository contributor statistics.

## Usage:

```shell
GitHubStats.exe [owner] [repo]
GitHubStats.exe [owner] [repo] [token]
```

**Argument**:

- **Owner** - The user or organization that owns the repository.
- **Repo** - The name of the repository.
- **Token** - The repository access token, if required.

**Example**:

```shell
GitHubStats.exe jessekingf github-stats
```

Output:

```shell
jessekingf
Commits: 4
Lines added: 1877
Lines deleted: 453
Lines changed: 2330
```

## Install

1. Install the [.NET 8.0 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
2. Download the latest [release](https://github.com/jessekingf/github-stats/releases).
