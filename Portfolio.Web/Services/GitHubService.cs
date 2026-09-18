using System.Net.Http.Json;

namespace Portfolio.Web.Services;

/// <summary>
/// Supplies the repo list for the Learning page.
///
/// Two sources, tried in order:
///
/// 1. repos.json, baked into the site by the deploy workflow. In production
///    this always exists, so visitors make zero GitHub API calls and the
///    60 requests per hour unauthenticated limit never applies, no matter
///    how much traffic the site gets. The data is as fresh as the last push.
///
/// 2. The live GitHub API, used only when repos.json is missing, which is
///    the case during local development since the file is generated at
///    deploy time rather than committed.
///
/// If both fail, this returns an empty list rather than throwing, and the
/// Learning page falls back to its hand written content. A portfolio should
/// not show an error because someone else's API had a bad day.
/// </summary>
public class GitHubService(HttpClient http)
{
    private const string User = "lyx0z";
    private const string BakedFile = "repos.json";

    private IReadOnlyList<GitHubRepo>? cache;

    /// <summary>True when the data came from the live API rather than the
    /// baked file. Handy while developing, not shown anywhere by default.</summary>
    public bool UsedLiveApi { get; private set; }

    public async Task<IReadOnlyList<GitHubRepo>> GetReposAsync()
    {
        if (cache is not null)
        {
            return cache;
        }

        cache = await TryBakedFileAsync() ?? await TryLiveApiAsync() ?? [];

        return cache;
    }

    private async Task<IReadOnlyList<GitHubRepo>?> TryBakedFileAsync()
    {
        try
        {
            // Relative path, so it resolves against <base href> and keeps
            // working whether the site is at a domain root or a subfolder.
            var repos = await http.GetFromJsonAsync<List<GitHubRepo>>(BakedFile);

            // A missing file on GitHub Pages returns the SPA fallback HTML
            // rather than a 404, which deserialises to null instead of
            // throwing, so an empty result counts as "not there".
            return repos is { Count: > 0 } ? repos : null;
        }
        catch
        {
            return null;
        }
    }

    private async Task<IReadOnlyList<GitHubRepo>?> TryLiveApiAsync()
    {
        try
        {
            var repos = await http.GetFromJsonAsync<List<GitHubRepo>>(
                $"https://api.github.com/users/{User}/repos?per_page=100&sort=pushed"
            );

            if (repos is { Count: > 0 })
            {
                UsedLiveApi = true;
                return repos;
            }

            return null;
        }
        catch
        {
            // Offline, or rate limited (403). Either way, give up quietly.
            return null;
        }
    }
}
