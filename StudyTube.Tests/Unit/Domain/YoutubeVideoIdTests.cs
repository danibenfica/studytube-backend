using NUnit.Framework;
using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Tests.Domain;

[TestFixture]
public class YouTubeVideoIdTests
{
    [TestCase("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
    [TestCase("https://youtu.be/dQw4w9WgXcQ")]
    [TestCase("https://www.youtube.com/embed/dQw4w9WgXcQ")]
    [TestCase("https://www.youtube.com/shorts/dQw4w9WgXcQ")]
    public void FromUrl_LinksValidos_ExtraiId(string url)
    {
        var id = YouTubeVideoId.FromUrl(url);

        Assert.That(id.Value, Is.EqualTo("dQw4w9WgXcQ"));
    }

    [TestCase("nao é link")]
    [TestCase("https://google.com")]
    public void FromUrl_LinkInvalido_LancaDomainException(string url)
    {
        Assert.Throws<DomainException>(() => YouTubeVideoId.FromUrl(url));
    }

    [Test]
    public void Create_IdComTamanhoErrado_LancaDomainException()
    {
        Assert.Throws<DomainException>(() => YouTubeVideoId.Create("abc"));
    }

    [Test]
    public void DoisIdsIguais_SaoIguais()
    {
        var primeiro = YouTubeVideoId.Create("dQw4w9WgXcQ");
        var segundo = YouTubeVideoId.Create("dQw4w9WgXcQ");

        Assert.That(primeiro, Is.EqualTo(segundo));
    }
}