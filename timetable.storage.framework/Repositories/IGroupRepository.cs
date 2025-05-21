using Timetable.Framework.Records;
using Timetable.Framework.Repositories;
using Timetable.Storage.Database.Entities;

namespace Timetable.Framework;

public interface IGroupRepository : IRepository<GroupRecord>
{

}

public interface IGroupMutationRepository : IGroupRepository, IMutationRepository<GroupRecord, GroupEntity>
{

}
