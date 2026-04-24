using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CodeController : ControllerBase
{
    private readonly CodeService _codeService;

    public CodeController(CodeService codeService)
    {
        _codeService = codeService;
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunCode(RunCodeDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _codeService.RunCodeAsync(userId, dto);

        return Ok(result);
    }
}
