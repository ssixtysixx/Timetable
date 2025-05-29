using Mapster;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

using Timetable.Framework.Records;
using Timetable.Storage.Database.Entities;
using Timetable.Storage.Framework.Repositories;

namespace Timetable.Storage.Database.Repositories;

public class UserRepository(IContextFactory contextFactory) : IUserRepository
{
    private readonly IContextFactory _contextFactory = contextFactory;

    public async Task<IEnumerable<UserRecord>> GetAsync(Expression<Func<UserAdminEntity, bool>> filter)
    {
        var context = _contextFactory.CreateContext();
        return await context.Set<UserAdminEntity>()
            .Where(filter)
            .ProjectToType<UserRecord>()
            .ToListAsync();
    }

}
