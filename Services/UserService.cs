using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaderboardUserDto>> GetLeaderboardAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.XP)
                .Take(10)
                .Select(u => new LeaderboardUserDto
                {
                    Username = u.Username,
                    XP = u.XP,
                    Level = u.Level,
                })
                .ToListAsync();
        }
    }
}