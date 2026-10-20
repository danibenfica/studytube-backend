using StudyTube.Domain.Exceptions;
using StudyTube.Domain.ValueObjects;

namespace StudyTube.Tests.Unit.Domain;

[TestFixture]
public class YouTubeVideoIdTests
{
    [TestCase("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
    [TestCase("https://youtu.be/dQw4w9WgXcQ")]
    [TestCase("https://www.youtube.com/embed/dQw4w9WgXcQ")]
    [TestCase("https://www.youtube.com/shorts/dQw4w9WgXcQ")]
    public void FromUrl_ValidLinks_ExtractsId(string url)
    {
        var id = YouTubeVideoId.FromUrl(url);

        Assert.That(id.Value, Is.EqualTo("dQw4w9WgXcQ"));
    }

    [TestCase("nao é link")]
    [TestCase("https://google.com")]
    public void FromUrl_InvalidLink_ThrowsDomainException(string url)
    {
        Assert.Throws<DomainException>(() => YouTubeVideoId.FromUrl(url));
    }

    [Test]
    public void Create_WrongLength_ThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => YouTubeVideoId.Create("abc"));
    }

    [Test]
    public void Equals_SameId_ReturnsTrue()
    {
        var first = YouTubeVideoId.Create("dQw4w9WgXcQ");
        var second = YouTubeVideoId.Create("dQw4w9WgXcQ");

        Assert.That(first, Is.EqualTo(second));
    }
}