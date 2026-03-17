using LearningPlatformAPI.Services;
using LearningPlatformAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatformAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
    {
        var course = await _courseService.CreateCourseAsync(dto);
        return Ok(course);
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses =await _courseService.GetCoursesAsync();
        return Ok(courses);
    } 

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }
}