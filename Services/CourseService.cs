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

    public async Task<Course> CreateCourseAsync(CreateCourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            AuthorId = dto.AuthorId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return course;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses   
            .Include(c => c.Modules)
            .ThenInclude(m => m.Lessons)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}