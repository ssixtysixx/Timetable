namespace Timetable.Framework.Records;

public class GroupDayRecord
{
	public required GroupRecord GroupRecord { get; init; }

	public required List<DayRecord> Records { get; init; }
}
