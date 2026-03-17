using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class Module
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int Order { get; set; }
    public List<Lesson> Lessons { get; set; } = new();
}