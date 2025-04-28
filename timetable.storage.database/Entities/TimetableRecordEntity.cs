namespace Timetable.Storage.Database.Entities;

public sealed class TimetableRecordEntity : EntityBase
{
	public DateTime TimeStamp { get; set; }

	public TeacherEntity Teacher { get; set; }

	public GroupEntity Group { get; set; }

	public PlaceEntity Place { get; set; }

	public DisciplineEntity Discipline { get; set; }
}
