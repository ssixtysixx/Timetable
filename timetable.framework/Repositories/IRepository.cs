namespace Timetable.Framework.Repositories;

public interface IRepository<TObject> 
	where TObject : class
{
	Task<long> CountAllAsync(CancellationToken cancellationToken);

	Task<IEnumerable<TObject>> GetAllAsync(CancellationToken cancellationToken);

	Task<TObject?> GetByIdAsync(long id, CancellationToken cancellationToken);
}
