namespace Timetable.Framework.Records;

using System;
using System.Collections.Generic;

public class DayScheduleDto
{
    public string DayOfWeek { get; set; } = default!;
    public DateTimeOffset Date { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public List<LessonDto> Lessons { get; set; } = new();
}
