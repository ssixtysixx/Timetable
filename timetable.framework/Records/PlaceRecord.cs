namespace Timetable.Framework.Records;

public record PlaceRecord
{
	public required PlaceType PlaceType { get; init; }

	public required string PlaceName { get; init; }
}
