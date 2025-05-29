namespace rasp.Controllers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;
using Timetable.Framework.Records;
using Timetable.Storage.Framework;

[Authorize]
public class RaspisAdminController(IRecordMutationRepository recordrepository) : Controller
{
    private readonly IRecordMutationRepository _recordrepository = recordrepository;

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
    public async Task<IActionResult> AddDaySchedule([FromBody] DayScheduleDto dto)
    {
        await _recordrepository.AddNewGroupRecord(dto);
        return Json(new { success = true });
    }
}
