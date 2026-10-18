using StudyTube.Domain.ValueObjects;
using StudyTube.Services.YouTube;

namespace StudyTube.Services.Interfaces;

public interface IYouTubeApiClient
{
    Task<YouTubeVideoInfo?> GetVideoAsync(YouTubeVideoId id, CancellationToken cancellationToken = default);
}