using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class CodeTask
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string ExpectedOutput { get; set; } = null!;

    public int LessonId { get; set; }
}
