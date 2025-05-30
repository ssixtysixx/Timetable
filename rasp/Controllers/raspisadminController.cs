namespace Rasp.Controllers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Framework;
using Timetable.Storage.Framework.Repositories;

using static Timetable.Framework.RaspisAdminController;

[Authorize]
public partial class RaspisAdminController(IRecordMutationRepository recordrepository,
    IDayRepository dayRepository) : Controller
{
    private readonly IRecordMutationRepository _recordrepository = recordrepository;
    private readonly IDayRepository _dayRepository = dayRepository;

    public async Task<IActionResult> Index()
    {
        Dictionary<string, List<GroupByDayRecords>> list = [];

        lock (this)
        {
            for (int i = 0; i < 7; i++)
            {
                var value = (
                    _recordrepository.GiveMeRecordForAllGroups(
                        DateTime.Now.AddDays(i),
                        CancellationToken.None
                    )
                ).Result;

                if (value == null)
                    continue;

                list[ScheduleDayConverter.GetRussianDayOfWeek(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"))] = (value);
            }
        }
        var groups = await _recordrepository.GetAllGroupsNames();

        var vm = new ScheduleViewModel
        {
            Days = [.. ScheduleDayConverter.DaysOfWeek],

            Groups = groups,
            Data = list
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult DeleteDay([FromBody] DeleteDayRequest request)
    {
        _dayRepository.DeleteRecord(request);
        return Ok();
    }


    public async Task<IActionResult> AddDaySchedule([FromBody] DayScheduleDto dto,[FromQuery] bool force = false)
    {
        bool exists = await _recordrepository.ExistsScheduleForGroupOnDate(dto);

        if (exists && !force)
        {
            return Json(new { success = false, message = "Расписание для этой группы на выбранную дату уже существует." });
        }

        await _recordrepository.AddNewGroupRecord(dto);
        return Json(new { success = true });
    }

}
