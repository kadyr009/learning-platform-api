public class QuizResponseDto
{
    public int Id { get; set; }
    public string Question { get; set; } = null!;
    public List<QuizOptionResponseDto> Options { get; set; } = new();
}

public class QuizOptionResponseDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
}

public class AnswerQuizResponseDto
{
    public bool Correct { get; set; }
}