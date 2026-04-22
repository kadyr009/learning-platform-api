using LearningPlatformAPI.Data;
using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatformAPI.Services
{
    public class QuizService
    {
        private readonly AppDbContext _context;
        private readonly GamificationService _gamificationService;
        private readonly AchievementService _achievementService;

        public QuizService(
            AppDbContext context, 
            GamificationService gamificationService,
            AchievementService achievementService)
        {
            _context = context;
            _gamificationService = gamificationService;
            _achievementService = achievementService;
        }

        public async Task<QuizResponseDto> CreateQuizAsync(CreateQuizDto dto)
        {
            var quiz = new Quiz
            {
                Question = dto.Question,
                LessonId = dto.LessonId,
                Options = dto.Options.Select(o => new QuizOption
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return new QuizResponseDto
            {
                Id = quiz.Id,
                Question = quiz.Question,
                Options = quiz.Options.Select(o => new QuizOptionResponseDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            };
        }

        public async Task<object> AnswerQuizAsync(int userId, AnswerQuizDto dto)
        {
            var option = await _context.QuizOptions
                .FirstOrDefaultAsync(o => o.Id == dto.SelectedOptionId);

            if (option == null)
                return new { success = false, message = "Ответ не найден" };

            bool isCorrect = option.IsCorrect;

            if (isCorrect)
            {
                await _gamificationService.AddXpForLessonAsync(userId); 
                await _achievementService.CheckQuizAchievement(userId);
            }

            return new AnswerQuizResponseDto
            {
                Correct = isCorrect
            };
        }

        public async Task<QuizResponseDto?> GetQuizByLessonId(int lessonId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.LessonId == lessonId);

            if (quiz == null) return null;

            return new QuizResponseDto
            {
                Id = quiz.Id,
                Question = quiz.Question,
                Options = quiz.Options.Select(o => new QuizOptionResponseDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            };
        }
    }
}