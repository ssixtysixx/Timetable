namespace Timetable.Framework;

public interface IDisciplineRepository
{

}

public interface IDisciplineMutationRepository : IDisciplineRepository
{
    
}
public class DisciplineRepository : IDisciplineMutationRepository
{
    
}