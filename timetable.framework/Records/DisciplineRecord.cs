namespace Timetable.Framework.Records;

public record DisciplineRecord
{
	public required string DisciplineCode { get; init; }

	public string DisciplineName { get; init; } = string.Empty;

	public int AllTimeHours { get; init; }
}
