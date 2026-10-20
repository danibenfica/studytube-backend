using StudyTube.Domain.Entities;
using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;
using StudyTube.Services.Interfaces;

namespace StudyTube.Services.Videos;

public class VideoService(IVideoRepository repository, IYouTubeApiClient youTube) : IVideoService{
    public async Task<List<VideoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var videos = await repository.GetAllAsync(cancellationToken);
        return videos.Select(VideoDto.FromEntity).ToList();
    }

    public async Task<VideoDto> AddFromUrlAsync(string url, CancellationToken cancellationToken = default)
    {
        var youTubeId = YouTubeVideoId.FromUrl(url);

        if (await repository.ExistsAsync(youTubeId, cancellationToken))
            throw new DomainException("Este vídeo já está na sua lista.");

        var info = await youTube.GetVideoAsync(youTubeId, cancellationToken)
                   ?? throw new DomainException("Vídeo não encontrado no YouTube.");

        var video = Video.Create(youTubeId, info.Title, info.ChannelTitle, info.ThumbnailUrl, info.Duration);

        await repository.AddAsync(video, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return VideoDto.FromEntity(video);
    }

    public async Task<VideoDto?> MarkAsWatchedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var video = await repository.GetByIdAsync(id, cancellationToken);
        if (video is null)
            return null;

        video.MarkAsWatched();
        await repository.SaveChangesAsync(cancellationToken);

        return VideoDto.FromEntity(video);
    }

    public async Task<VideoDto?> MarkAsUnwatchedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var video = await repository.GetByIdAsync(id, cancellationToken);
        if (video is null)
            return null;

        video.MarkAsUnwatched();
        await repository.SaveChangesAsync(cancellationToken);

        return VideoDto.FromEntity(video);
    }
}