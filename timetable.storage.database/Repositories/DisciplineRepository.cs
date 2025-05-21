using Mapster;
using Microsoft.EntityFrameworkCore;
using Timetable.Framework;
using Timetable.Framework.Records;

namespace Timetable.Storage.Framework;

public sealed class DisciplineRepository(TimetableDBContext timetableDbContext) : IDisciplineRepository
{
	private TimetableDBContext _context = timetableDbContext;

	public Task<long> CountAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<DisciplineRecord>> GetAllAsync(CancellationToken cancellationToken)
	{
		var entities = _context.DisciplinesEntity.AsNoTracking();

		return entities.Select(x => x.Adapt<DisciplineRecord>());
	}

	public Task<DisciplineRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

