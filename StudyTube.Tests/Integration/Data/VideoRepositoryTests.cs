using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StudyTube.Data;
using StudyTube.Data.Repositories;
using StudyTube.Domain.Entities;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Tests.Integration.Data;

[TestFixture]
public class VideoRepositoryTests
{
    private SqliteConnection _connection = null!;
    private StudyTubeDbContext _context = null!;
    private VideoRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<StudyTubeDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new StudyTubeDbContext(options);
        _context.Database.EnsureCreated();
        _repository = new VideoRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _connection.Dispose();
    }

    private static Video CriarVideo() =>
        Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), "Aula de DDD", "Canal", "", TimeSpan.FromMinutes(10));

    [Test]
    public async Task AddAsync_SalvaVideoNoBanco()
    {
        await _repository.AddAsync(CriarVideo());
        await _repository.SaveChangesAsync();

        var videos = await _repository.GetAllAsync();

        Assert.That(videos, Has.Count.EqualTo(1));
        Assert.That(videos[0].Title, Is.EqualTo("Aula de DDD"));
    }

    [Test]
    public async Task ExistsAsync_VideoSalvo_RetornaTrue()
    {
        await _repository.AddAsync(CriarVideo());
        await _repository.SaveChangesAsync();

        var existe = await _repository.ExistsAsync(YouTubeVideoId.Create("dQw4w9WgXcQ"));

        Assert.That(existe, Is.True);
    }

    [Test]
    public async Task GetByIdAsync_IdInexistente_RetornaNull()
    {
        var video = await _repository.GetByIdAsync(Guid.NewGuid());

        Assert.That(video, Is.Null);
    }

    [Test]
    public async Task MarkAsWatched_DepoisDeSalvar_ContinuaAssistidoNoBanco()
    {
        var video = CriarVideo();
        await _repository.AddAsync(video);
        await _repository.SaveChangesAsync();

        video.MarkAsWatched();
        await _repository.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var doBanco = await _repository.GetByIdAsync(video.Id);

        Assert.That(doBanco, Is.Not.Null);
        Assert.That(doBanco!.Watched, Is.True);
    }
}