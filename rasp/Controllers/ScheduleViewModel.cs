namespace rasp.Controllers;

using System.Globalization;

using Timetable.Framework.Records;

public class ScheduleViewModel
{
    public List<string> Days { get; set; }

    public List<string> Groups { get; set; }

    public List<GroupDayRecord> Data { get; set; }

    public static string[] DaysOfWeek => 
    [
    "Понедельник",
    "Вторник",
    "Среда",
    "Четверг",
    "Пятница",
    "Суббота",
    "Воскресение"
    ];
}

public static class ScheduleDayConverter
{
    /// <summary>Преобразует день недели в русский.</summary>
    /// <param name="dateString">День недели в формате "yyyy-MM-dd".</param>
    /// <returns>Один день из списка.</returns>
    public static string? GetRussianDayOfWeek(string dateString)
    {
        if (DateTime.TryParseExact(dateString, "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var date))
        {
            int dayOfWeekIndex = ((int)date.DayOfWeek + 6) % 7;
            return ScheduleViewModel.DaysOfWeek[dayOfWeekIndex];
        }

        return null;
    }
}

