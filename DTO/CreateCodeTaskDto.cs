namespace LearningPlatformAPI.DTO;
public class CreateCodeTaskDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ExpectedOutput { get; set; } = null!;
    public int LessonId { get; set; }
}
