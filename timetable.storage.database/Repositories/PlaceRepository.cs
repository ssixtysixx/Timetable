using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework;

public class PlaceRepository : IPlaceMutationRepository
{
	public Task<PlaceRecord> AddAsync(PlaceEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<long> CountAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<PlaceRecord>> GetAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<PlaceRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<PlaceRecord> RemoveAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<PlaceRecord> RemoveAsync(PlaceRecord entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<PlaceRecord> UpdateAsync(PlaceEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
