using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services;

public class CodeService
{
    private readonly AppDbContext _context;
    private readonly Judge0Service _judgeService;
    private readonly GamificationService _gamificationService;

    public CodeService(
        AppDbContext context,
        Judge0Service judgeService,
        GamificationService gamificationService)
    {
        _context = context;
        _judgeService = judgeService;
        _gamificationService = gamificationService;
    }

    public async Task<object> RunCodeAsync(int userId, RunCodeDto dto)
    {
        var task = await _context.CodeTasks
            .FirstOrDefaultAsync(t => t.Id == dto.CodeTaskId);

        if (task == null)
            return new { success = false, message = "Задание не найдено" };

        var output = await _judgeService.RunCodeAsync(dto.SourceCode);

        bool isCorrect = output != null &&
                            output.Trim() == task.ExpectedOutput.Trim();

        if (isCorrect)
        {
            await _gamificationService.AddXpForLessonAsync(userId);
        }

        return new
        {
            correct = isCorrect,
            output = output
        };
    }

    public async Task<CodeTask> CreateAsync(CreateCodeTaskDto dto)
    {
        var task = new CodeTask
        {
            Title = dto.Title,
            Description = dto.Description,
            ExpectedOutput = dto.ExpectedOutput,
            LessonId = dto.LessonId
        };

        _context.CodeTasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<CodeTask?> GetByLessonIdAsync(int lessonId)
    {
        return await _context.CodeTasks
            .FirstOrDefaultAsync(t => t.LessonId == lessonId);
    }
}
