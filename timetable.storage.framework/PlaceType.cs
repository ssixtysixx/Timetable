using System.ComponentModel;

namespace Timetable.Storage.Framework;

public enum PlaceType
{
	[Description("Аудитория")]
	Auditorium,

	[Description("Спортивный зал")]
	SportHall,

	[Description("Актовый зал")]
	AssemblyHall,
}
