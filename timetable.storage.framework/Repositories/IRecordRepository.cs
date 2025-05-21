using Timetable.Framework.Records;

namespace Timetable.Framework;

public interface IRecordRepository
{
	Task<IEnumerable<DaySingleRecord>> GetAllDaySingleRecords(CancellationToken cancellationToken);

	Task<IEnumerable<DayRecord>> GetAllDayRecords(CancellationToken cancellationToken);

	Task<DayRecord> GetDayRecordsByDate(DateTime date, CancellationToken cancellationToken);
}

public interface IRecordMutationRepository : IRecordRepository
{

}
