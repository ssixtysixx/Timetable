using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;

public class RaspisController : Controller
{
    private readonly IDisciplineRepository _Disciplinerepository;
    private readonly IGroupRepository _Grouprepository;
    private readonly IPlaceRepository _Placerepository;
    private readonly IRecordRepository _Recordrepository;
    private readonly ITeacherRepository _Teacherrepository;
    public RaspisController(IDisciplineRepository Disciplinerepository, IGroupRepository Grouprepository, IPlaceRepository Placerepository, IRecordRepository Recordrepository, ITeacherRepository Teacherrepository)
    {
        _Disciplinerepository = Disciplinerepository;
        _Grouprepository = Grouprepository;
        _Placerepository = Placerepository;
        _Recordrepository = Recordrepository;
        _Teacherrepository = Teacherrepository;
    }
    public ActionResult Index()
    {
        // Создаем расписание
        var schedule = new List<Raspisanie>
        {
            new Raspisanie { Id = 1, DayOfWeek = "Понедельник", Time = "09:00 - 10:30", Subject = "Математика", Teacher = "Иванов", Room = "101" },
            new Raspisanie { Id = 2, DayOfWeek = "Понедельник", Time = "10:40 - 12:10", Subject = "Физика", Teacher = "Петров", Room = "202" },
            new Raspisanie { Id = 3, DayOfWeek = "Вторник", Time = "09:00 - 10:30", Subject = "Программирование", Teacher = "Сидоров", Room = "305" },
            new Raspisanie { Id = 4, DayOfWeek = "Среда", Time = "13:00 - 14:30", Subject = "Английский", Teacher = "Кузнецова", Room = "104" },
            new Raspisanie { Id = 5, DayOfWeek = "Четверг", Time = "10:40 - 12:10", Subject = "История", Teacher = "Васильева", Room = "207" }
        };

        // Преобразуем в SelectListItem для ListBox
        var listItems = schedule.Select(x => new SelectListItem
        {
            Text = $"{x.DayOfWeek} | {x.Time} | {x.Subject} | {x.Teacher} | Ауд. {x.Room}",
            Value = x.Id.ToString()
        }).ToList();

        ViewBag.ScheduleItems = listItems;

        return View();
    }

    [HttpPost]
    public ActionResult Index(List<int> SelectedItems)
    {
        // Обработка выбранных занятий (например, запись в БД)
        if (SelectedItems != null)
        {
            foreach (var id in SelectedItems)
            {
                // Логика обработки выбранных элементов
            }
        }
        return RedirectToAction("Index");
    }
}