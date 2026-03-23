using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LessonController : ControllerBase
{
    private readonly LessonService _lessonService;

    public LessonController(LessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson(CreateLessonDto dto)
    {
        var lesson = await _lessonService.CreateLessonAsync(dto);
        return Ok(lesson);
    }

    [HttpGet("module/{moduleId}")]
    public async Task<IActionResult> GetLessons(int moduleId)
    {
        var lessons = await _lessonService.GetLessonsByModuleAsync(moduleId);
        return Ok(lessons);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(int id, UpdateLessonDto dto)
    {
        var success = await _lessonService.UpdateLessonAsync(id, dto);

        if (!success)
            return NotFound("Урок не найден");

        return Ok("Урок обновлён");
    }
}