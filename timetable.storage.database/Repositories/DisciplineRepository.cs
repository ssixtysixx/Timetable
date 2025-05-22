using Mapster;
using Microsoft.EntityFrameworkCore;
using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database;

namespace Timetable.Storage.Framework;

public sealed class DisciplineRepository(IContextFactory contextFactory) : IDisciplineRepository
{
	private IContextFactory _contextFactory = contextFactory;

	public Task<long> CountAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<DisciplineRecord>> GetAllAsync(CancellationToken cancellationToken)
	{
		using var context = _contextFactory.CreateContext();

		var entities = context.DisciplinesEntity.AsNoTracking();

		return entities.Select(x => x.Adapt<DisciplineRecord>());
	}

	public Task<DisciplineRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}

