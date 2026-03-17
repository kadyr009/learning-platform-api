using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class Lesson
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public int ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public int Order { get; set; } 
}