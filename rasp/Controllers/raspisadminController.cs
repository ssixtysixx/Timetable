namespace rasp.Controllers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;
using Timetable.Framework.Records;

[Authorize]
public class RaspisAdminController : Controller
{
    public IActionResult Index()
    {
        var group = new GroupRecord { Name = "0110" };
        var records = new List<DayRecord>
        {
            new DayRecord
            {
                Date = DateTime.Now,
                SingleRecords = new List<DaySingleRecord>
                {
                    new DaySingleRecord
                    {
                        Number = 1,
                        Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
                        Group = group,
                        Teacher = new TeacherRecord { Name = "FIO" },
                        Place = new PlaceRecord
                        {
                            PlaceName = "place1",
                            PlaceType = PlaceType.SportHall
                        }
                    },
                    new DaySingleRecord
                    {
                        Number = 2,
                        Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
                        Group = group,
                        Teacher = new TeacherRecord { Name = "FIO" },
                        Place = new PlaceRecord
                        {
                            PlaceName = "place1",
                            PlaceType = PlaceType.SportHall
                        }
                    }
                }
            },
            new DayRecord
            {
                Date = DateTime.Now.AddDays(1),
                SingleRecords = new List<DaySingleRecord>
                {
                    new DaySingleRecord
                    {
                        Number = 1,
                        Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
                        Group = group,
                        Teacher = new TeacherRecord { Name = "FIO" },
                        Place = new PlaceRecord
                        {
                            PlaceName = "place1",
                            PlaceType = PlaceType.SportHall
                        }
                    }
                }
            }
        };

        var vm = new ScheduleViewModel
        {
            Days = records
                .Select(r => r.Date.ToString("dddd", new CultureInfo("ru-RU")))
                .Distinct()
                .ToList(),

            Groups = new List<string> { group.Name },
            Data = new List<GroupDayRecord>
            {
                new GroupDayRecord { GroupRecord = group, Records = records }
            }
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult AddDaySchedule([FromBody] DayScheduleDto dto)
    {
        return Json(new { success = true });
    }
}

public class LessonDto
{
    public int Number { get; set; }
    public string DisciplineCode { get; set; } = default!;
    public string TeacherName { get; set; } = default!;
    public string PlaceName { get; set; } = default!;
}

public class DayScheduleDto
{
    public string DayOfWeek { get; set; } = default!;
    public DateTimeOffset Date { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public List<LessonDto> Lessons { get; set; } = new();
}
