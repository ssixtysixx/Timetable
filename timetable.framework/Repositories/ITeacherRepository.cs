namespace Timetable.Framework;

public interface ITeacherRepository
{

}

public interface ITeacherMutationRepository : ITeacherRepository
{

}
public class TeacherRepository : ITeacherMutationRepository
{

}