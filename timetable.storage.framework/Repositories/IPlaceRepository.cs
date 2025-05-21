using Timetable.Framework.Records;
using Timetable.Framework.Repositories;
using Timetable.Storage.Database.Entities;

namespace Timetable.Framework;

public interface IPlaceRepository : IRepository<PlaceRecord>
{
	
}

public interface IPlaceMutationRepository : IPlaceRepository, IMutationRepository<PlaceRecord, PlaceEntity>
{
}