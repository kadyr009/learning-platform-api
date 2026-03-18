using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatformAPI.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgressController : ControllerBase
{
    private readonly ProgressService _progressService;

    public ProgressController(ProgressService progressService)
    {
        _progressService = progressService;
    }

    [HttpPost("complete")]
    public async Task<IActionResult> CompleteLesson(CompleteLessonDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _progressService.CompleteLessonAsync(userId, dto.LessonId);

        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyProgress()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var progress = await _progressService.GetUserProgress(userId);

        return Ok(progress);
    }

    [HttpGet("course-progress")]
    public async Task<IActionResult> GetCourseProgress(int courseId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        var progress = await _progressService.GetCourseProgress(userId, courseId);

        return Ok(progress);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetCourseWithProgress(int courseId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var course = await _progressService.GetCourseWithProgressAsync(userId, courseId);
        if (course == null)
            return NotFound();
            
        return Ok(course);
    }
}