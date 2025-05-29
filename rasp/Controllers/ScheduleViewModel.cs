namespace rasp.Controllers;

using Timetable.Framework.Records;

public class ScheduleViewModel
{
    public List<string> Days { get; set; }

    public List<string> Groups { get; set; }

    public Dictionary<string, List<GroupByDayRecords>> Data { get; set; }

}

public class DateWithName
{
    public string RuName { get; set; }

    public DateWithName(string RawDateTime)
    {
        RuName = ScheduleDayConverter.GetRussianDayOfWeek(RawDateTime);
    }
}

