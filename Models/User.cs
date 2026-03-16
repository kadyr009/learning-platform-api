using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    public int XP { get; set; } = 0;
    public int Level { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}