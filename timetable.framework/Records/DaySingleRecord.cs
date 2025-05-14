namespace Timetable.Framework.Records;

public record DayRecord
{
	public required DateTime Date { get; init; }

	public required List<DaySingleRecord> SingleRecords { get; init; }
}

public record DaySingleRecord
{
	public required int Number { get; init; }

	public required DisciplineRecord Discipline { get; init; }

	public required GroupRecord Group { get; init; }

	public required TeacherRecord Teacher { get; init; }

	public required PlaceRecord Place { get; init; }
}
