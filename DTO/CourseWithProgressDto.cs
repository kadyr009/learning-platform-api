namespace LearningPlatformAPI.DTO;

public class LessonProgressDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public int Order { get; set; }
    public bool IsCompleted { get; set; }
}

public class ModuleWithProgressDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Order { get; set; }
    public List<LessonProgressDto> Lessons { get; set; } = new();
}

public class CourseWithProgressDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public List<ModuleWithProgressDto> Modules { get; set; } = new();
    public int ProgressPercent { get; set; }
}