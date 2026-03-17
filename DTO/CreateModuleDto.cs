using Microsoft.Net.Http.Headers;

namespace LearningPlatformAPI.DTO;

public class CreateModuleDto
{
    public string Title { get; set; } = null!;
    public int CourseId { get; set; }
    public int Order { get; set; }
}