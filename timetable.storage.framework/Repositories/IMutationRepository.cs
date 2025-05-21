using Timetable.Storage.Database.Entities;

namespace Timetable.Framework.Repositories;

public interface IMutationRepository<TObject, TEntity> : IRepository<TObject> 
	where TObject : class
	where TEntity : EntityBase
{
	Task<TObject> AddAsync(TEntity entity, CancellationToken cancellationToken);

	Task<TObject> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

	Task<TObject> RemoveAsync(long id, CancellationToken cancellationToken);

	Task<TObject> RemoveAsync(TObject entity, CancellationToken cancellationToken);
}
