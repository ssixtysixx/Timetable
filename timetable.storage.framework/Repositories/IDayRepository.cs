using static Timetable.Framework.RaspisAdminController;

namespace Timetable.Storage.Framework.Repositories;

public interface IDayRepository
{
    public Task<bool> DeleteRecord(DeleteDayRequest deleteDayRequest);
}
