using Timetable.Framework;

namespace Timetable.Storage.Framework.Repositories;

public interface IDayRepository
{
    public Task<bool> DeleteRecord(DeleteDayRequest deleteDayRequest);
}
