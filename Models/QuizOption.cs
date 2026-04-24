using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class QuizOption
{
    [Key]
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
}
