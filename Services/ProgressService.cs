using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class ProgressService
{
    private readonly AppDbContext _context;

    public ProgressService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserLessonProgress> CompleteLessonAsync(CompleteLessonDto dto)
    {
        var existing = await _context.UserLessonProgresses
            .FirstOrDefaultAsync(p =>
                p.UserId == dto.UserId &&
                p.LessonId == dto.LessonId);

        if (existing != null)
        {
            existing.IsCompleted = true;
            existing.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        var progress = new UserLessonProgress
        {
            UserId = dto.UserId,
            LessonId = dto.LessonId,
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _context.UserLessonProgresses.Add(progress);
        await _context.SaveChangesAsync();

        return progress;
    }

    public async Task<List<UserLessonProgress>> GetUserProgress(int userId)
    {
        return await _context.UserLessonProgresses
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<double> GetCourseProgress(int userId, int courseId)
    {
        var totalLessons = await _context.Lessons
            .Where(l => l.Module.CourseId == courseId)
            .CountAsync();

        if (totalLessons == 0)
            return 0;

        var completedLessons = await _context.UserLessonProgresses
            .Where(p => p.UserId == userId && 
                        p.IsCompleted &&
                        p.Lesson.Module.CourseId == courseId)
            .CountAsync();
        
        return (double)completedLessons / totalLessons * 100;
    }
}