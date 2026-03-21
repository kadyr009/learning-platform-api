namespace LearningPlatformAPI.DTO;

public class CreateCourseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
}