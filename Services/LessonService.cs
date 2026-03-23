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

    public async Task<bool> UpdateLessonAsync(int id, UpdateLessonDto dto)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson ==null)
            return false;

        lesson.Title = dto.Title;
        lesson.Content = dto.Content;
        lesson.VideoUrl = dto.VideoUrl;
        lesson.Order = dto.Order;

        await _context.SaveChangesAsync();

        return true;
    }
}
