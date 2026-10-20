using StudyTube.Domain.Entities;
using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;
using StudyTube.Services.Interfaces;
using StudyTube.Services.Videos;
using StudyTube.Services.YouTube;

namespace StudyTube.Tests.Unit.Services;

[TestFixture]
public class VideoServiceTests
{
    private FakeVideoRepository _repository = null!;
    private FakeYouTubeApiClient _youTube = null!;
    private VideoService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new FakeVideoRepository();
        _youTube = new FakeYouTubeApiClient();
        _service = new VideoService(_repository, _youTube);
    }

    [Test]
    public async Task AddFromUrlAsync_ValidLink_SavesVideo()
    {
        var dto = await _service.AddFromUrlAsync("https://youtu.be/dQw4w9WgXcQ");

        Assert.That(dto.Title, Is.EqualTo("Aula de DDD"));
        Assert.That(_repository.Videos, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task AddFromUrlAsync_DuplicateVideo_ThrowsDomainException()
    {
        await _service.AddFromUrlAsync("https://youtu.be/dQw4w9WgXcQ");

        Assert.ThrowsAsync<DomainException>(() =>
            _service.AddFromUrlAsync("https://youtu.be/dQw4w9WgXcQ"));
    }

    [Test]
    public void AddFromUrlAsync_VideoNotFoundOnYouTube_ThrowsDomainException()
    {
        _youTube.ShouldFindVideo = false;

        Assert.ThrowsAsync<DomainException>(() =>
            _service.AddFromUrlAsync("https://youtu.be/dQw4w9WgXcQ"));
    }

    [Test]
    public async Task MarkAsWatchedAsync_UnknownId_ReturnsNull()
    {
        var result = await _service.MarkAsWatchedAsync(Guid.NewGuid());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task MarkAsWatchedAsync_ExistingVideo_MarksAsWatched()
    {
        var dto = await _service.AddFromUrlAsync("https://youtu.be/dQw4w9WgXcQ");

        var resultado = await _service.MarkAsWatchedAsync(dto.Id);

        Assert.That(resultado!.Watched, Is.True);
    }

    private class FakeVideoRepository : IVideoRepository
    {
        public List<Video> Videos { get; } = [];

        public Task<List<Video>> GetAllAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(Videos.ToList());

        public Task<Video?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Videos.FirstOrDefault(v => v.Id == id));

        public Task<bool> ExistsAsync(YouTubeVideoId youTubeId, CancellationToken cancellationToken = default) =>
            Task.FromResult(Videos.Any(v => v.YouTubeId == youTubeId));

        public Task AddAsync(Video video, CancellationToken cancellationToken = default)
        {
            Videos.Add(video);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }

    private class FakeYouTubeApiClient : IYouTubeApiClient
    {
        public bool ShouldFindVideo { get; set; } = true;

        public Task<YouTubeVideoInfo?> GetVideoAsync(YouTubeVideoId id, CancellationToken cancellationToken = default) =>
            Task.FromResult(ShouldFindVideo
                ? new YouTubeVideoInfo("Aula de DDD", "Canal", "", TimeSpan.FromMinutes(10))
                : null);
    }
}