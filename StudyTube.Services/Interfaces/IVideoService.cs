using StudyTube.Services.Videos;

namespace StudyTube.Services.Interfaces;

public interface IVideoService
{
    Task<List<VideoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VideoDto> AddFromUrlAsync(string url, CancellationToken cancellationToken = default);
    Task<VideoDto?> MarkAsWatchedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VideoDto?> MarkAsUnwatchedAsync(Guid id, CancellationToken cancellationToken = default);
}