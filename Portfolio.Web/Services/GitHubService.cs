using System.Net.Http.Json;

namespace Portfolio.Web.Services;

/// <summary>
/// Fetches the public repos for one GitHub user.
///
/// Two things worth knowing about this:
///
/// 1. One request, not one per repo. The /users/{name}/repos endpoint returns
///    every repo in a single response. GitHub allows 60 unauthenticated
///    requests per hour per IP address, so asking once instead of seven times
///    is the difference between working and getting rate limited.
///
/// 2. It never throws. If GitHub is down, rate limited, or the visitor is
///    offline, this returns an empty list and the page falls back to the
///    static content. A portfolio should not show an error because someone
///    else's API had a bad day.
/// </summary>
public class GitHubService(HttpClient http)
{
    private const string User = "lyx0z";

    private IReadOnlyList<GitHubRepo>? cache;

    public async Task<IReadOnlyList<GitHubRepo>> GetReposAsync()
    {
        if (cache is not null)
        {
            return cache;
        }

        try
        {
            var repos = await http.GetFromJsonAsync<List<GitHubRepo>>(
                $"https://api.github.com/users/{User}/repos?per_page=100&sort=pushed"
            );

            cache = repos ?? [];
        }
        catch
        {
            // Network error, rate limit, or malformed response.
            // Cache the empty result so we don't retry on every render.
            cache = [];
        }

        return cache;
    }
}
