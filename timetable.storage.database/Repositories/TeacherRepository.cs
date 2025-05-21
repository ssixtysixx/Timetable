using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework;

public class TeacherRepository : ITeacherMutationRepository
{
	public Task<TeacherRecord> AddAsync(TeacherEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<long> CountAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<TeacherRecord>> GetAllAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<TeacherRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<TeacherRecord> RemoveAsync(long id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<TeacherRecord> RemoveAsync(TeacherRecord entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<TeacherRecord> UpdateAsync(TeacherEntity entity, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
