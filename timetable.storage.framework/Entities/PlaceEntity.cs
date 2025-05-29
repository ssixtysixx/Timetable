using Timetable.Framework;

namespace Timetable.Storage.Database.Entities;

public sealed class PlaceEntity : EntityBase
{
	public PlaceType PlaceType { get; set; }

	public string PlaceName { get; set; }
}
