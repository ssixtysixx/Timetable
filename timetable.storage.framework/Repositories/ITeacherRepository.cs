using Timetable.Framework.Records;
using Timetable.Framework.Repositories;
using Timetable.Storage.Database.Entities;

namespace Timetable.Framework;

public interface ITeacherRepository
{

}

public interface ITeacherMutationRepository : ITeacherRepository, IMutationRepository<TeacherRecord, TeacherEntity>
{

}
