using Mapster;
using Microsoft.EntityFrameworkCore;
using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database;

namespace Timetable.Storage.Framework;

public sealed class RecordRepository(IContextFactory contextFactory) : IRecordMutationRepository
{
	private IContextFactory _contextFactory = contextFactory;

	public Task<IEnumerable<DayRecord>> GetAllDayRecords(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<DaySingleRecord>> GetAllDaySingleRecords(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<DayRecord> GetDayRecordsByDate(DateTime date, CancellationToken cancellationToken)
	{
		using var context = _contextFactory.CreateContext();

		var records = context
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

		return new DayRecord { Date = date, SingleRecords = daySingeRecords.ToList() };
	}
}
