using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework;

public class GroupRepository : IGroupMutationRepository
{
	public Task<GroupRecord> AddAsync(GroupEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<long> CountAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<GroupRecord>> GetAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<GroupRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<GroupRecord> RemoveAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<GroupRecord> RemoveAsync(GroupRecord entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<GroupRecord> UpdateAsync(GroupEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
