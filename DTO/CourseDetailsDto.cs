public class CourseDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public int AuthorId { get; set; }
    public List<ModuleDetailsDto> Modules { get; set; } = new();
}

public class ModuleDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }= null!;
    public int CourseId { get; set; }
    public int Order { get; set; }

    public List<LessonDetailsDto> Lessons { get; set; } = new();
}

public class LessonDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public int ModuleId { get; set; }
    public int Order { get; set; } 
}