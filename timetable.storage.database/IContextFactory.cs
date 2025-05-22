using Timetable.Storage.Framework;

namespace Timetable.Storage.Database;

public interface IContextFactory
{
	TimetableDBContext CreateContext();
}
