using Microsoft.AspNetCore.Mvc;
using StudyTube.Domain.Exceptions;
using StudyTube.Services.Videos;

namespace StudyTube.Api.Controllers;

public record AddVideoRequest(string Url);

[ApiController]
[Route("api/[controller]")]
public class VideosController(VideoService videoService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<VideoDto>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await videoService.GetAllAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<VideoDto>> Add(AddVideoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var video = await videoService.AddFromUrlAsync(request.Url, cancellationToken);
            return Created($"/api/videos/{video.Id}", video);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPatch("{id:guid}/watched")]
    public async Task<ActionResult<VideoDto>> MarkAsWatched(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var video = await videoService.MarkAsWatchedAsync(id, cancellationToken);
            return video is null ? NotFound() : Ok(video);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}/watched")]
    public async Task<ActionResult<VideoDto>> MarkAsUnwatched(Guid id, CancellationToken cancellationToken)
    {
        var video = await videoService.MarkAsUnwatchedAsync(id, cancellationToken);
        return video is null ? NotFound() : Ok(video);
    }
}