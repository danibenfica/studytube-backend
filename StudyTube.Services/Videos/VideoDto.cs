using StudyTube.Domain.Entities;

namespace StudyTube.Services.Videos;

public record VideoDto(
    Guid Id,
    string YouTubeId,
    string Title,
    string ChannelTitle,
    string ThumbnailUrl,
    TimeSpan Duration,
    bool Watched,
    DateTime AddedAt,
    DateTime? WatchedAt)
{
    public static VideoDto FromEntity(Video video) => new(
        video.Id,
        video.YouTubeId.Value,
        video.Title,
        video.ChannelTitle,
        video.ThumbnailUrl,
        video.Duration,
        video.Watched,
        video.AddedAt,
        video.WatchedAt);
}