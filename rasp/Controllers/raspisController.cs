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
using Timetable.Storage.Framework;

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
    public IRecordMutationRepository _recordMutationRepository { get; }

    public RaspisController(
        IUserRepository userRepository,
        IRecordMutationRepository recordMutationRepository
        )
    {

        _userRepository = userRepository;
        _recordMutationRepository = recordMutationRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var vm = await ActivateAsync();
        return View(vm);
    }

    public async Task<ScheduleViewModel> ActivateAsync()
    {
        Dictionary<string, List<GroupByDayRecords>> list = [];

        lock (this)
        {
            for (int i = 0; i < 7; i++)
            {
                var value = (
                    _recordMutationRepository.GiveMeRecordForAllGroups(
                        DateTime.Now.AddDays(i),
                        CancellationToken.None
                    )
                ).Result;

                if (value == null)
                    continue;

                list[ScheduleDayConverter.GetRussianDayOfWeek(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"))] = (value);
            }
        }
        var groups = await _recordMutationRepository.GetAllGroupsNames();

        var vm = new ScheduleViewModel
        {
            Days = [.. ScheduleDayConverter.DaysOfWeek],

            Groups = groups,
            Data = list
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
