using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;
using Timetable.Framework.Records;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Timetable.Storage.Framework.Repositories;
using Timetable.Storage.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Globalization;

namespace rasp.Controllers;

public class RaspisController : Controller
{
    private readonly IDisciplineRepository _Disciplinerepository;
    private readonly IGroupRepository _grouprepository;
    private readonly IPlaceRepository _Placerepository;
    private readonly IRecordRepository _Recordrepository;
    private readonly ITeacherRepository _Teacherrepository;
    private readonly IUserRepository _userRepository;

    //TODO:
    public List<GroupDayRecord> Records { get; set; }

    public RaspisController(
        IUserRepository userRepository
        )
    {

        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var vm = await ActivateAsync();
        return View(vm);
    }

    public async Task<ScheduleViewModel> ActivateAsync()
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
        return vm;
    }

    [HttpPost]
    public async Task<ActionResult> Login(string login, string password)
    {
        Expression<Func<UserAdminEntity, bool>> filter = c => c.Login == login && c.Password == password;

        var users = await _userRepository.GetAsync(filter);
        if (users.Any())
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, login),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Json(new { success = true});
        }
        else
        {
            return Json(new { success = false, message = "Пользователя не существует или неверный пароль." });
        }
    }
}
