using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LeaderboardUserDto>> GetLeaderboardAsync()
    {
        var users = await _context.Users
            .OrderByDescending(u => u.XP)
            .Take(10)
            .Select(u => new LeaderboardUserDto
            {
                Username = u.Username,
                XP = u.XP,
                Level = u.Level
            })
            .ToListAsync();

        for (int i = 0; i < users.Count; i++)
        {
            users[i].Rank = i + 1;
        }

        return users;
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        var completedLessons = await _context.UserLessonProgresses
            .CountAsync(p => p.UserId == userId && p.IsCompleted);

        var achievementsCount = await _context.UserAchievements
            .CountAsync(a => a.UserId == userId);

        return new UserProfileDto
        {
            Username = user.Username,
            Email = user.Email,
            XP = user.XP,
            Level = user.Level,
            CompletedLessons = completedLessons,
            AchievementsCount = achievementsCount
        };
    }
}
