using Mapster;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

    public Task<UserRecord> AddAsync(UserAdminEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<long> CountAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRecord>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    public Task<UserRecord?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserRecord> RemoveAsync(long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserRecord> RemoveAsync(UserRecord entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserRecord> UpdateAsync(UserAdminEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
