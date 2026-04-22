using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; } = null!;

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        public List<QuizOption> Options { get; set; } = new();
    }
}