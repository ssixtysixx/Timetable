using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Storage.Database.Entities;

public sealed class DisciplineEntity : EntityBase
{
	public required string DisciplineCode { get; set; }

	public string DisciplineName { get; set; } = string.Empty;

	public int AllTimeHours { get; set; }
}
