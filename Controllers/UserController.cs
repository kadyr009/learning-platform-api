using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AchievementService _achievementService;

    public UserController(UserService userService, AchievementService achievementService)
    {
        _userService = userService;
        _achievementService = achievementService;
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard()
    {
        var users = await _userService.GetLeaderboardAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("achievements")]
    public async Task<IActionResult> GetMyAchievements()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var achievements = await _achievementService.GetUserAchievements(userId);

        return Ok(achievements);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var user = await _userService.GetUserProfileAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}
