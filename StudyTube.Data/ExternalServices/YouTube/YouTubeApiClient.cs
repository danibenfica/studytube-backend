using System.Net.Http.Json;
using System.Xml;
using StudyTube.Domain.ValueObjects;
using StudyTube.Services.Interfaces;
using StudyTube.Services.YouTube;

namespace StudyTube.Data.ExternalServices.YouTube;

public class YouTubeApiClient(HttpClient httpClient, YouTubeOptions options) : IYouTubeApiClient
{
    public async Task<YouTubeVideoInfo?> GetVideoAsync(YouTubeVideoId id, CancellationToken cancellationToken = default)
    {
        var url = $"videos?part=snippet,contentDetails&id={id.Value}&key={options.ApiKey}";

        var response = await httpClient.GetFromJsonAsync<YouTubeResponse>(url, cancellationToken);
        var item = response?.Items.FirstOrDefault();

        if (item is null)
            return null;

        var thumbnail = item.Snippet.Thumbnails.High?.Url
                        ?? item.Snippet.Thumbnails.Default?.Url
                        ?? string.Empty;

        return new YouTubeVideoInfo(
            item.Snippet.Title,
            item.Snippet.ChannelTitle,
            thumbnail,
            XmlConvert.ToTimeSpan(item.ContentDetails.Duration));
    }

    private record YouTubeResponse(List<YouTubeItem> Items);
    private record YouTubeItem(Snippet Snippet, ContentDetails ContentDetails);
    private record Snippet(string Title, string ChannelTitle, Thumbnails Thumbnails);
    private record Thumbnails(Thumbnail? Default, Thumbnail? High);
    private record Thumbnail(string Url);
    private record ContentDetails(string Duration);
}