namespace rasp.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Globalization;

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
                    new DaySingleRecord { Number = 1, Discipline = new DisciplineRecord{ DisciplineCode="dis01" }, Group=group, Teacher=new TeacherRecord{Name="FIO"}, Place=new PlaceRecord{PlaceName="place1",PlaceType=PlaceType.SportHall} },
                    new DaySingleRecord { Number = 2, Discipline = new DisciplineRecord{ DisciplineCode="dis01" }, Group=group, Teacher=new TeacherRecord{Name="FIO"}, Place=new PlaceRecord{PlaceName="place1",PlaceType=PlaceType.SportHall} }
                }
            },
            new DayRecord
            {
                Date = DateTime.Now.AddDays(1),
                SingleRecords = new List<DaySingleRecord>
                {
                    new DaySingleRecord { Number = 1, Discipline = new DisciplineRecord{ DisciplineCode="dis01" }, Group=group, Teacher=new TeacherRecord{Name="FIO"}, Place=new PlaceRecord{PlaceName="place1",PlaceType=PlaceType.SportHall} }
                }
            }
        };

        var groupDayRecord = new GroupDayRecord
        {
            GroupRecord = group,
            Records = records
        };

        var daysList = records
            .Select(r => r.Date.ToString("dddd", new CultureInfo("ru-RU")))
            .Distinct()
            .ToList();
        var groupsList = new List<string> { group.Name };

        var vm = new ScheduleViewModel
        {
            Days = daysList,
            Groups = groupsList,
            Data = new List<GroupDayRecord> { groupDayRecord }
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Save([FromBody] List<LessonDto> lessons)
    {
        foreach (var s in lessons)
        {
            Debug.WriteLine(
                $"Группа={s.Group}, День={s.DayOfWeek}, Пара№={s.Number}, " +
                $"Дисциплина={s.DisciplineCode}, Преподаватель={s.TeacherName}, Кабинет={s.PlaceName}"
            );
        }
        return Json(new { success = true });
    }


}

public class LessonDto
{
    public string DayOfWeek { get; set; } = default!;

    public string Group { get; set; } = default!;

    public int Number { get; set; }

    public string DisciplineCode { get; set; } = default!;

    public string TeacherName { get; set; } = default!;

    public string PlaceName { get; set; } = default!;
}
