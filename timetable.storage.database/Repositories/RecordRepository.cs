using Mapster;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Globalization;

using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Database;
using Timetable.Storage.Database.Entities;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Timetable.Storage.Framework;

public sealed class RecordRepository(IContextFactory contextFactory) : IRecordMutationRepository
{
    private IContextFactory _contextFactory = contextFactory;

    public async Task<bool> ExistsScheduleForGroupOnDate(DayScheduleDto dayScheduleDto)
    {
        using var context = _contextFactory.CreateContext();

        return await context.TimetableRecords
            .Include(r => r.Group)
            .AnyAsync(r => r.Group.GroupName == dayScheduleDto.GroupName && r.TimeStamp.Date == dayScheduleDto.Date.Date);
    }



    public async Task<bool> AddNewGroupRecord(DayScheduleDto dayScheduleDto)
    {
        using var context = _contextFactory.CreateContext();

        var groupsCache = new Dictionary<string, GroupEntity>();
        var disciplinesCache = new Dictionary<string, DisciplineEntity>();
        var teachersCache = new Dictionary<string, TeacherEntity>();
        var placesCache = new Dictionary<string, PlaceEntity>();

        if (!groupsCache.TryGetValue(dayScheduleDto.GroupName, out GroupEntity group))
        {
            group = await context.GroupsEntity
                .FirstOrDefaultAsync(g => g.GroupName == dayScheduleDto.GroupName);

            if (group == null)
            {
                group = new GroupEntity
                {
                    GroupName = dayScheduleDto.GroupName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                context.GroupsEntity.Add(group);
            }

            groupsCache[dayScheduleDto.GroupName] = group;
        }

        foreach (var lesson in dayScheduleDto.Lessons)
        {
            if (!disciplinesCache.TryGetValue(lesson.DisciplineCode, out var discipline))
            {
                discipline = await context.DisciplinesEntity
                    .FirstOrDefaultAsync(d => d.DisciplineCode == lesson.DisciplineCode);

                if (discipline == null)
                {
                    discipline = new DisciplineEntity
                    {
                        DisciplineCode = lesson.DisciplineCode,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    context.DisciplinesEntity.Add(discipline);
                }

                disciplinesCache[lesson.DisciplineCode] = discipline;
            }

            if (!teachersCache.TryGetValue(lesson.TeacherName, out var teacher))
            {
                teacher = await context.TeachersEntity
                    .FirstOrDefaultAsync(t => t.Name == lesson.TeacherName);

                if (teacher == null)
                {
                    teacher = new TeacherEntity
                    {
                        Name = lesson.TeacherName,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    context.TeachersEntity.Add(teacher);
                }

                teachersCache[lesson.TeacherName] = teacher;
            }

            if (!placesCache.TryGetValue(lesson.PlaceName, out var place))
            {
                place = await context.PlacesEntity
                    .FirstOrDefaultAsync(p => p.PlaceName == lesson.PlaceName);

                if (place == null)
                {
                    place = new PlaceEntity
                    {
                        PlaceName = lesson.PlaceName,
                        PlaceType = PlaceType.Auditorium,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    context.PlacesEntity.Add(place);
                }

                placesCache[lesson.PlaceName] = place;
            }

            var record = new TimetableRecordEntity
            {
                Group = group,
                Discipline = discipline,
                Teacher = teacher,
                Place = place,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TimeStamp = dayScheduleDto.Date.DateTime,
            };
            context.TimetableRecords.Add(record);
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<DayRecord> GetDayRecordsByDate(DateTime date, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateContext();

        var records = context
            .TimetableRecords
            .AsNoTracking()
            .Include(x => x.Group)
            .Include(x => x.Teacher)
            .Include(x => x.Discipline)
            .Include(x => x.Place)
            .AsEnumerable()
            .Where(x => x.TimeStamp.Date == date.Date);

        if (!records.Any()) return null;

        var copyOfRecords = records.ToList();

        var daySingeRecords = records.Select(x => new DaySingleRecord
        {
            Number = copyOfRecords.Index().First(s => s.Item.Id == x.Id).Index,
            Discipline = x.Discipline.Adapt<DisciplineRecord>(),
            Group = x.Group.Adapt<GroupRecord>(),
            Teacher = x.Teacher.Adapt<TeacherRecord>(),
            Place = x.Place.Adapt<PlaceRecord>(),
        });

        //What the fuck is this fucking bullshit.
        return new DayRecord { Date = date, SingleRecords = [.. daySingeRecords] };
    }

    public async Task<List<GroupByDayRecords>> GiveMeRecordForAllGroups(DateTime date, CancellationToken cancellationToken)
    {
        using var context = _contextFactory.CreateContext();

        var records = context
            .TimetableRecords
            .AsNoTracking()
            .Include(x => x.Group)
            .Include(x => x.Teacher)
            .Include(x => x.Discipline)
            .Include(x => x.Place)
            .AsEnumerable()
            .Where(x => x.TimeStamp.Date == date.Date)
            .ToList();

        var copyOfRecords = records.ToList();
        var copyOfRecords2 = records.ToList();

        var groups = context
            .GroupsEntity.AsNoTracking().AsEnumerable().ToList();

        List<GroupByDayRecords> IhateSSD = [];

        foreach (var group in groups)
        {
            var recordForDay = records.Where(record => record.Group.GroupName == group.GroupName).Select(rec => new DaySingleRecord
            {
                Number = copyOfRecords.Index().First(s => s.Item.Id == rec.Id).Index,
                Discipline = rec.Discipline.Adapt<DisciplineRecord>(),
                Group = rec.Group.Adapt<GroupRecord>(),
                Teacher = rec.Teacher.Adapt<TeacherRecord>(),
                Place = rec.Place.Adapt<PlaceRecord>(),
            });

            IhateSSD.Add(new()
            {
                Date = date,
                Group = group.Adapt<GroupRecord>(),
                SingleRecords = [.. recordForDay],
            });
        }
        return IhateSSD;
    }

    public Task<List<string>> GetAllGroupsNames()
    {
        using var context = _contextFactory.CreateContext();

        return Task.FromResult(context.GroupsEntity.Select(x => x.GroupName).ToList());
    }

    public async Task SeedTestData()
    {
        var groups = new List<string> { "Группа 101", "Группа 202", "Группа 303" };

        var startDate = new DateTime(2025, 6, 2);
        var schedules = new List<DayScheduleDto>();

        foreach (var group in groups)
        {
            for (int dayOffset = 0; dayOffset < 5; dayOffset++) // Пн-Пт
            {
                var date = startDate.AddDays(dayOffset);
                var dayOfWeek = date.ToString("dddd", new CultureInfo("ru-RU"));
                var daySchedule = new DayScheduleDto
                {
                    GroupName = group,
                    Date = date,
                    DayOfWeek = dayOfWeek,
                    Lessons = GenerateLessonsForDay()
                };
                schedules.Add(daySchedule);
            }
        }

        foreach (var schedule in schedules)
        {
            await AddNewGroupRecord(schedule);
        }
    }

    private static List<LessonDto> GenerateLessonsForDay()
    {
        return
    [
        new() {
            Number = 1,
            DisciplineCode = "Программная инженирия и прочие штучки",
            TeacherName = "Иванов И.И.",
            PlaceName = "5.112"
        },
        new() {
            Number = 2,
            DisciplineCode = "Программная инженирия и прочие штучки",
            TeacherName = "Иванов И.И.",
            PlaceName = "5.112"
        },
        new() {
            Number = 3,
            DisciplineCode = "Программная инженирия и прочие штучки",
            TeacherName = "Иванов И.И.",
            PlaceName = "5.112"
        }
    ];
    }

    public async Task<bool> AddGroupIfNotExistAsync(string groupName)
    {
        using var context = _contextFactory.CreateContext();

        GroupEntity group = await context.GroupsEntity
            .FirstOrDefaultAsync(g => g.GroupName == groupName);

        if (group == null)
        {
            group = new GroupEntity
            {
                GroupName = groupName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.GroupsEntity.Add(group);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
