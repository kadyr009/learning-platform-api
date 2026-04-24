namespace LearningPlatformAPI.DTO;

public class CreateQuizDto
{
    public string Question { get; set; } = null!;
    public int LessonId { get; set; }
    public List<CreateQuizOptionDto> Options { get; set; } = new();
}

public class CreateQuizOptionDto
{
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}

