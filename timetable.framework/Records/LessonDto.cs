namespace rasp.Controllers;

public class LessonDto
{
    public int Number { get; set; }
    public string DisciplineCode { get; set; } = default!;
    public string TeacherName { get; set; } = default!;
    public string PlaceName { get; set; } = default!;
}
