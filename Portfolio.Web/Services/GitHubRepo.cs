using System.Text.Json.Serialization;

namespace Portfolio.Web.Services;

/// <summary>
/// The slice of GitHub's repo JSON that this site actually uses.
/// GitHub sends snake_case names, so anything that is not a plain
/// lowercase word needs a JsonPropertyName to map it onto a C# property.
/// </summary>
public class GitHubRepo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = "";

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("pushed_at")]
    public DateTimeOffset? PushedAt { get; set; }

    [JsonPropertyName("stargazers_count")]
    public int Stars { get; set; }

    /// <summary>
    /// "3 days ago" style text for the last push. Returns null when GitHub
    /// gave us no date, so the UI can simply leave the badge out.
    /// </summary>
    public string? LastPushedText
    {
        get
        {
            if (PushedAt is null)
            {
                return null;
            }

            var days = (DateTimeOffset.UtcNow - PushedAt.Value).TotalDays;

            return days switch
            {
                < 1 => "today",
                < 2 => "yesterday",
                < 14 => $"{(int)days} days ago",
                < 60 => $"{(int)(days / 7)} weeks ago",
                _ => $"{(int)(days / 30)} months ago"
            };
        }
    }
}
