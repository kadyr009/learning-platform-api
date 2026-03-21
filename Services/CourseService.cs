using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class CourseService
{
    private readonly AppDbContext _context;

    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Course> CreateCourseAsync(int userId, CreateCourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            AuthorId = userId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return course;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<CourseDetailsDto?> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Modules)
            .ThenInclude(m => m.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return null;

        return new CourseDetailsDto
        {
            Id = course.Id,
            Title = course.Title,
            Modules = course.Modules.Select(m => new ModuleDetailsDto
            {
                Id = m.Id,
                Title = m.Title,
                Lessons = m.Lessons.Select(l => new LessonDetailsDto
                {
                    Id = l.Id,
                    Title = l.Title
                }).ToList()
            }).ToList()
        };
    }
}