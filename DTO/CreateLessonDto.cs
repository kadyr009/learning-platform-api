namespace LearningPlatformAPI.DTO
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }

        public int ModuleId { get; set; }
        public int Order { get; set; }
    }
}