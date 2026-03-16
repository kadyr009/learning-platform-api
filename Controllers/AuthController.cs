using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return result.IsSuccess ? Ok(new { message = result.Message}) : BadRequest(new { message = result.Message});
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return result.IsSuccess ? Ok(new { message = result.Message}) : BadRequest(new { message = result.Message});
    }
}