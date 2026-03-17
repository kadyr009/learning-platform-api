using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Module> Modules { get; set; } = new();
}