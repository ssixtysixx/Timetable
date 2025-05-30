namespace Timetable.Framework;

using System;

public partial class RaspisAdminController
{
    public class DeleteDayRequest
    {
        public string Group { get; set; }

        public string Day { get; set; }

        public DateTime Date { get; set; }
    }
}
