namespace Rasp.Controllers;

using Timetable.Framework.Records;

public class ScheduleViewModel
{
    public List<string> Days { get; set; }

    public List<string> Groups { get; set; }

    public Dictionary<string, List<GroupByDayRecords>> Data { get; set; }

}


