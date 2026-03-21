public class CourseDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public List<ModuleDetailsDto> Modules { get; set; } =new();
}

public class ModuleDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }= null!;
    public List<LessonDetailsDto> Lessons { get; set; } = new();
}

public class LessonDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}