using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }
}