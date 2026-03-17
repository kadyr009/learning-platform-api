using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly ModuleService _moduleService;

    public ModuleController(ModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateModule(CreateModuleDto dto)
    {
        var module = await _moduleService.CreateModuleAsync(dto);
        return Ok(module);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetMudules(int courseId)
    {
        var modules = await _moduleService.GetModulesByCoourseId(courseId);
        return Ok(modules);
    }
}