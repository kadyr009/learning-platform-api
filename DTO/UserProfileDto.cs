namespace LearningPlatformAPI.DTO;

public class UserProfileDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int XP { get; set; }
    public int Level { get; set; }

    public int CompletedLessons { get; set; }
    public int AchievementsCount { get; set; }
}
