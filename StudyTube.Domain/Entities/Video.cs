using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Domain.Entities;

public class Video
{
    public Guid Id { get; private set; }
    public YouTubeVideoId YouTubeId { get; private set; } = null!;
    public string Title { get; private set; } = string.Empty;
    public string ChannelTitle { get; private set; } = string.Empty;
    public string ThumbnailUrl { get; private set; } = string.Empty;
    public TimeSpan Duration { get; private set; }
    public bool Watched { get; private set; }
    public DateTime AddedAt { get; private set; }
    public DateTime? WatchedAt { get; private set; }

    private Video() { }

    public static Video Create(YouTubeVideoId youTubeId, string title, string channelTitle,
        string thumbnailUrl, TimeSpan duration)
    {
        if (youTubeId is null)
            throw new DomainException("O ID do YouTube é obrigatório.");
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("O título é obrigatório.");
        if (duration < TimeSpan.Zero)
            throw new DomainException("A duração não pode ser negativa.");

        return new Video
        {
            Id = Guid.NewGuid(),
            YouTubeId = youTubeId,
            Title = title.Trim(),
            ChannelTitle = channelTitle?.Trim() ?? string.Empty,
            ThumbnailUrl = thumbnailUrl ?? string.Empty,
            Duration = duration,
            AddedAt = DateTime.UtcNow
        };
    }

    public void MarkAsWatched()
    {
        if (Watched)
            throw new DomainException("Este vídeo já foi marcado como assistido.");

        Watched = true;
        WatchedAt = DateTime.UtcNow;
    }

    public void MarkAsUnwatched()
    {
        Watched = false;
        WatchedAt = null;
    }
}