namespace Timetable.Framework;

using System.Globalization;

public static class ScheduleDayConverter
{
    public static string[] DaysOfWeek =>
        ["Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресение"];

    /// <summary>Преобразует день недели в русский.</summary>
    /// <param name="dateString">День недели в формате "yyyy-MM-dd".</param>
    /// <returns>Один день из списка.</returns>
    public static string? GetRussianDayOfWeek(string dateString)
    {
        if (
            DateTime.TryParseExact(
                dateString,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date
            )
        )
        {
            int dayOfWeekIndex = ((int)date.DayOfWeek + 6) % 7;
            return DaysOfWeek[dayOfWeekIndex];
        }

        return null;
    }

    public static string[] GetDays()
    {
        return [.. DaysOfWeek.ToList().Select(r => GetRussianDayOfWeek(r))];  
    } 
}
