namespace Timetable.Framework.Records;

public record TeacherRecord
{
	public required string Name { get; init; }

	public string Comments { get; init; } = string.Empty;
}
