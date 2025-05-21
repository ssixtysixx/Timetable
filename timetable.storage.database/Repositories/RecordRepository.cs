using Mapster;
using Microsoft.EntityFrameworkCore;
using Timetable.Framework;
using Timetable.Framework.Records;

namespace Timetable.Storage.Framework;

public sealed class RecordRepository : IRecordMutationRepository
{
	private TimetableDBContext _context;

	public Task<IEnumerable<DayRecord>> GetAllDayRecords(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<DaySingleRecord>> GetAllDaySingleRecords(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<DayRecord> GetDayRecordsByDate(DateTime date, CancellationToken cancellationToken)
	{
		var records = _context
			.TimetableRecords
			.AsNoTracking()
			.Include(x => x.Group)
			.Include(x => x.Teacher)
			.Include(x => x.Discipline)
			.Include(x => x.Place)
			.AsEnumerable()
			.Where(x => x.TimeStamp.Date == date.Date);

		var daySingeRecords = records.Select(x => new DaySingleRecord
		{
			Number = records.Index().First(s => s.Item.Id == x.Id).Index,
			Discipline = x.Discipline.Adapt<DisciplineRecord>(),
			Group = x.Group.Adapt<GroupRecord>(),
			Teacher = x.Teacher.Adapt<TeacherRecord>(),
			Place = x.Place.Adapt<PlaceRecord>(),
		});

		return Task.FromResult(new DayRecord { Date = date, SingleRecords = daySingeRecords.ToList() });
	}
}
