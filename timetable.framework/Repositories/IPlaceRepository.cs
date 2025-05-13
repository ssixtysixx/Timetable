namespace Timetable.Framework;

public interface IPlaceRepository
{
}

public interface IPlaceMutationRepository : IPlaceRepository
{
}
public class PlaceRepository : IPlaceMutationRepository
{

}