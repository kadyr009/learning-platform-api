using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformAPI.Services;

[ApiController]
[Route("api/[controller]")]
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
        var result = await _progressService.CompleteLessonAsync(dto);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserProgress(int userId)
    {
        var progress = await _progressService.GetUserProgress(userId);
        return Ok(progress);
    }

    [HttpGet("course-progress")]
    public async Task<IActionResult> GetCourseProgress(int userId, int courseId)
    {
        var progress = await _progressService.GetCourseProgress(userId, courseId);
        return Ok(progress);
    }

    [HttpGet("course/{courseId}/user/{userId}")]
    public async Task<IActionResult> GetCourseWithProgress(int courseId, int userId)
    {
        var course = await _progressService.GetCourseWithProgressAsync(userId, courseId);
        if (course == null)
            return NotFound();
            
        return Ok(course);
    }
}