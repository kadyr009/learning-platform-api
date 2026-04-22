using System.ComponentModel.DataAnnotations;

namespace LearningPlatformAPI.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}