using Microsoft.EntityFrameworkCore;
using StudyTube.Domain.Entities;
using StudyTube.Domain.ValueObjects;
using StudyTube.Services.Interfaces;

namespace StudyTube.Data.Repositories;

public class VideoRepository(StudyTubeDbContext context) : IVideoRepository
{
    public Task<List<Video>> GetAllAsync(CancellationToken cancellationToken = default) =>
        context.Videos.OrderByDescending(v => v.AddedAt).ToListAsync(cancellationToken);

    public Task<Video?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Videos.FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

    public Task<bool> ExistsAsync(YouTubeVideoId youTubeId, CancellationToken cancellationToken = default) =>
        context.Videos.AnyAsync(v => v.YouTubeId == youTubeId, cancellationToken);

    public async Task AddAsync(Video video, CancellationToken cancellationToken = default) =>
        await context.Videos.AddAsync(video, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}