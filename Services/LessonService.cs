using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Data;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class LessonService
{
    private readonly AppDbContext _context;

    public LessonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Lesson> CreateLessonAsync(CreateLessonDto dto)
    {
        var lesson = new Lesson
        {
            Title = dto.Title,
            Content = dto.Content,
            VideoUrl = dto.VideoUrl,
            ModuleId = dto.ModuleId,
            Order = dto.Order
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return lesson;
    }

    public async Task<List<Lesson>> GetLessonsByModuleAsync(int moduleId)
    {
        return await _context.Lessons
            .Where(l => l.ModuleId == moduleId)
            .ToListAsync();
    }
}
