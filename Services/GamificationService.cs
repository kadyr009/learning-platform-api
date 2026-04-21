using LearningPlatformAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services
{
    public class GamificationService
    {
        private readonly AppDbContext _context;

        public GamificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddXpForLessonAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return;

            user.XP += 10;

            user.Level = CalculateLevel(user.XP);

            await _context.SaveChangesAsync();
        }

        private int CalculateLevel(int xp)
        {
            return xp / 100 + 1;
        }
    }
}