using LearningPlatformAPI.Data;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services
{
    public class AchievementService
    {
        private readonly AppDbContext _context;

        public AchievementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CheckLessonAchievements(int userId)
        {
            var completedLessons = await _context.UserLessonProgresses
                .CountAsync(p => p.UserId == userId && p.IsCompleted);

            // 1 урок
            if (completedLessons >= 1)
                await GiveAchievement(userId, 1);

            // 5 уроков
            if (completedLessons >= 5)
                await GiveAchievement(userId, 2);
        }

        public async Task CheckXpAchievements(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null && user.XP >= 100)
                await GiveAchievement(userId, 4);
        }

        public async Task CheckQuizAchievement(int userId)
        {
            await GiveAchievement(userId, 3);
        }

        private async Task GiveAchievement(int userId, int achievementId)
        {
            var exists = await _context.UserAchievements
                .AnyAsync(a => a.UserId == userId && a.AchievementId == achievementId);

            if (exists)
                return;

            _context.UserAchievements.Add(new UserAchievement
            {
                UserId = userId,
                AchievementId = achievementId
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserAchievement>> GetUserAchievements(int userId)
        {
            return await _context.UserAchievements
                .Include(a => a.Achievement)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}