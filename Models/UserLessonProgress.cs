using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class UserLessonProgress
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedAt { get; set; }
}