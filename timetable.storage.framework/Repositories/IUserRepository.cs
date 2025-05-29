using System.Linq.Expressions;

using Timetable.Framework.Records;
using Timetable.Framework.Repositories;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserRecord>> GetAsync(Expression<Func<UserAdminEntity, bool>> filter);
}
