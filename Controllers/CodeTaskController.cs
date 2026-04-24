using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CodeTaskController : ControllerBase
{
    private readonly CodeService _service;

    public CodeTaskController(CodeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCodeTaskDto dto)
    {
        var task = await _service.CreateAsync(dto);
        return Ok(task);
    }

    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetByLesson(int lessonId)
    {
        var task = await _service.GetByLessonIdAsync(lessonId);

        if (task == null)
            return NotFound();

        return Ok(task);
    }
}
