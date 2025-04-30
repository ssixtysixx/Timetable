namespace Timetable.Framework.Repositories;

public interface IMutationRepository<TObject> : IRepository<TObject> where TObject : class
{
	Task<TObject> AddAsync(object entity, CancellationToken cancellationToken);

	Task<TObject> RemoveAsync(long id, CancellationToken cancellationToken);

	Task<TObject> RemoveAsync(TObject entity, CancellationToken cancellationToken);
}
