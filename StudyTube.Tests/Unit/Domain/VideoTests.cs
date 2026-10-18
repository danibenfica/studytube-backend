using NUnit.Framework;
using StudyTube.Domain.Entities;
using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Tests.Domain;

[TestFixture]
public class VideoTests
{
    private static Video CriarVideo() =>
        Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), "Aula de DDD", "Canal", "", TimeSpan.FromMinutes(10));

    [Test]
    public void Create_ComDadosValidos_CriaVideoNaoAssistido()
    {
        var video = CriarVideo();

        Assert.That(video.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(video.Watched, Is.False);
    }

    [Test]
    public void Create_SemTitulo_LancaDomainException()
    {
        Assert.Throws<DomainException>(() =>
            Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), " ", "Canal", "", TimeSpan.Zero));
    }

    [Test]
    public void Create_DuracaoNegativa_LancaDomainException()
    {
        Assert.Throws<DomainException>(() =>
            Video.Create(YouTubeVideoId.Create("dQw4w9WgXcQ"), "Aula", "Canal", "", TimeSpan.FromMinutes(-1)));
    }

    [Test]
    public void MarkAsWatched_MarcaComoAssistido()
    {
        var video = CriarVideo();

        video.MarkAsWatched();

        Assert.That(video.Watched, Is.True);
        Assert.That(video.WatchedAt, Is.Not.Null);
    }

    [Test]
    public void MarkAsWatched_DuasVezes_LancaDomainException()
    {
        var video = CriarVideo();
        video.MarkAsWatched();

        Assert.Throws<DomainException>(() => video.MarkAsWatched());
    }

    [Test]
    public void MarkAsUnwatched_DesmarcaVideo()
    {
        var video = CriarVideo();
        video.MarkAsWatched();

        video.MarkAsUnwatched();

        Assert.That(video.Watched, Is.False);
        Assert.That(video.WatchedAt, Is.Null);
    }
}