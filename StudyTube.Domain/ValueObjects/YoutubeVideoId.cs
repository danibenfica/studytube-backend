using System.Text.RegularExpressions;
using System.Web;
using StudyTube.Domain.Exceptions;

namespace StudyTube.Domain.ValueObjects;

public sealed record YouTubeVideoId
{
    private static readonly Regex IdRegex = new(@"^[A-Za-z0-9_-]{11}$", RegexOptions.Compiled);

    public string Value { get; }

    private YouTubeVideoId(string value) => Value = value;

    public static YouTubeVideoId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IdRegex.IsMatch(value))
            throw new DomainException("ID do YouTube inválido.");

        return new YouTubeVideoId(value);
    }

    public static YouTubeVideoId FromUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            throw new DomainException("URL inválida.");

        var host = uri.Host.ToLowerInvariant();
        string? id = null;

        if (host == "youtu.be")
            id = uri.AbsolutePath.Trim('/');
        else if (host is "youtube.com" or "www.youtube.com" or "m.youtube.com")
        {
            if (uri.AbsolutePath == "/watch")
                id = HttpUtility.ParseQueryString(uri.Query)["v"];
            else if (uri.AbsolutePath.StartsWith("/embed/") || uri.AbsolutePath.StartsWith("/shorts/"))
                id = uri.Segments[^1].Trim('/');
        }

        if (id is null)
            throw new DomainException("Não é uma URL de vídeo do YouTube.");

        return Create(id);
    }

    public override string ToString() => Value;
}