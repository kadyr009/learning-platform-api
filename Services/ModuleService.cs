using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class ModuleService
{
    private readonly AppDbContext _context;

    public ModuleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Module> CreateModuleAsync(CreateModuleDto dto)
    {
        var module = new Module
        {
            Title = dto.Title,
            CourseId = dto.CourseId,
            Order = dto.Order
        };

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        return module;
    }

    public async Task<List<Module>> GetModulesByCoourseId(int courseId)
    {
        return await _context.Modules
            .Where(m => m.CourseId == courseId)
            .ToListAsync();
    }
}