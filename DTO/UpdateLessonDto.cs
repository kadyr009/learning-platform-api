namespace LearningPlatformAPI.DTO
{
    public class UpdateLessonDto
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int Order { get; set; }
    }
}