namespace LearningPlatformAPI.DTO
{
    public class LeaderboardUserDto
    {
        public string Username { get; set; } = null!;
        public int XP { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
    }
}