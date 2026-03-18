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

    public async Task<UserLessonProgress> CompleteLessonAsync(int userId, int lessonId)
    {
        var existing = await _context.UserLessonProgresses
            .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.LessonId == lessonId);

        if (existing != null)
        {
            existing.IsCompleted = true;
            existing.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        var progress = new UserLessonProgress
        {
            UserId = userId,
            LessonId = lessonId,
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

    public async Task<CourseWithProgressDto?> GetCourseWithProgressAsync(int userId, int courseId)
    {
        var course = await _context.Courses
            .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
            .FirstOrDefaultAsync(c => c.Id == courseId);
        
        if (course == null)
            return null;

        var userProgress = await _context.UserLessonProgresses
            .Where(p => p.UserId == userId)
            .ToListAsync();

        var courseDto = new CourseWithProgressDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Category = course.Category,
            Modules = course.Modules
                .OrderBy(m => m.Order)
                .Select(m => new ModuleWithProgressDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Order = m.Order,
                    Lessons = m.Lessons
                        .OrderBy(l => l.Order)
                        .Select(l => new LessonProgressDto
                        {
                            Id = l.Id,
                            Title = l.Title,
                            Content = l.Content,
                            VideoUrl = l.VideoUrl,
                            Order = l.Order,
                            IsCompleted = userProgress.Any(p => p.LessonId == l.Id && p.IsCompleted)
                        })
                        .ToList()
                })
                .ToList()
        };

        var totalLessons = courseDto.Modules.Sum(m => m.Lessons.Count);
        var completedLessons = courseDto.Modules.Sum(m => m.Lessons.Count(l => l.IsCompleted));

        courseDto.ProgressPercent = totalLessons == 0 ? 0 : (double)completedLessons / totalLessons * 100;

        return courseDto;
    }
}