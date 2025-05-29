namespace Timetable.Framework.Records;

public class GroupDayRecord
{
    public required GroupRecord GroupRecord { get; init; }

    public required List<DayRecord> Records { get; init; }
}

public record DayRecord
{
	public required DateTime Date { get; init; }

	public GroupRecord Group => SingleRecords.FirstOrDefault().Group;

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

public record GroupByDayRecords
{
    public  GroupRecord Group { get; init; }

    public DateTime Date { get; init; }

    public required List<DaySingleRecord> SingleRecords { get; init; }


}