using StudyTube.Domain.Entities;
using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;
namespace StudyTube.Tests.Unit.Domain;

[TestFixture]
public class VideoTests
{
    private static Video CreateVideo() =>
        Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), "Aula de DDD", "Canal", "", TimeSpan.FromMinutes(10));

    [Test]
    public void Create_WithValidData_CreatesUnwatchedVideo()
    {
        var video = CreateVideo();

        Assert.That(video.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(video.Watched, Is.False);
    }

    [Test]
    public void Create_WithoutTitle_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() =>
            Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), " ", "Canal", "", TimeSpan.Zero));
    }

    [Test]
    public void Create_NegativeDuration_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() =>
            Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), "Aula", "Canal", "", TimeSpan.FromMinutes(-1)));
    }

    [Test]
    public void MarkAsWatched_MarksVideoAsWatched()
    {
        var video = CreateVideo();

        video.MarkAsWatched();

        Assert.That(video.Watched, Is.True);
        Assert.That(video.WatchedAt, Is.Not.Null);
    }

    [Test]
    public void MarkAsWatched_Twice_ThrowsDomainException()
    {
        var video = CreateVideo();
        video.MarkAsWatched();

        Assert.Throws<DomainException>(() => video.MarkAsWatched());
    }

    [Test]
    public void MarkAsUnwatched_MarksVideoAsUnwatched()
    {
        var video = CreateVideo();
        video.MarkAsWatched();

        video.MarkAsUnwatched();

        Assert.That(video.Watched, Is.False);
        Assert.That(video.WatchedAt, Is.Null);
    }
}