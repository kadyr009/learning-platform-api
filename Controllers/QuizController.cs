using LearningPlatformAPI.DTO;
using LearningPlatformAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz(CreateQuizDto dto)
        {
            var quiz = await _quizService.CreateQuizAsync(dto);
            return Ok(quiz);
        }

        [HttpGet("lesson/{lessonId}")]
        public async Task<IActionResult> GetQuiz(int lessonId)
        {
            var quiz = await _quizService.GetQuizByLessonId(lessonId);

            if (quiz == null)
                return NotFound();

            return Ok(quiz);
        }

        [HttpPost("answer")]
        public async Task<IActionResult> AnswerQuiz(AnswerQuizDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _quizService.AnswerQuizAsync(userId, dto);

            return Ok(result);
        }
    }
}