using Timetable.Framework.Records;
using Timetable.Framework.Repositories;
using Timetable.Storage.Database.Entities;

namespace Timetable.Framework;

public interface IDisciplineRepository : IRepository<DisciplineRecord>
{

}

public interface IDisciplineMutationRepository : IDisciplineRepository, IMutationRepository<DisciplineRecord, DisciplineEntity>
{
    
}