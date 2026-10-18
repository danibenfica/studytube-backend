using StudyTube.Domain.Entities;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Services.Interfaces;

public interface IVideoRepository
{
    Task<List<Video>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Video?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(YouTubeVideoId youTubeId, CancellationToken cancellationToken = default);
    Task AddAsync(Video video, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}