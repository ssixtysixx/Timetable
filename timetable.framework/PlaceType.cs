using System.ComponentModel;

namespace Timetable.Framework;

public enum PlaceType
{
	[Description("Аудитория")]
	Auditorium,

	[Description("Спортивный зал")]
	SportHall,

	[Description("Актовый зал")]
	AssemblyHall,
}
