using Microsoft.EntityFrameworkCore;

using Timetable.Framework;
using Timetable.Storage.Framework.Repositories;

namespace Timetable.Storage.Database.Repositories;

public sealed class DayRepository(IContextFactory contextFactory) : IDayRepository
{
    private IContextFactory _contextFactory = contextFactory;

    public async Task<bool> DeleteRecord(DeleteDayRequest deleteDayRequest)
    {
        try
        {
            await using var context = _contextFactory.CreateContext();

            var group = await context.GroupsEntity
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GroupName == deleteDayRequest.Group);

            if (group == null) return false;

            var recordsToDelete = await context
                .TimetableRecords
                .Where(x => x.TimeStamp.Date == deleteDayRequest.Date 
                && x.Group.GroupName == deleteDayRequest.Group)
                .ToListAsync();

            if (recordsToDelete.Count == 0) return true;

            context.TimetableRecords.RemoveRange(recordsToDelete);
            await context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
