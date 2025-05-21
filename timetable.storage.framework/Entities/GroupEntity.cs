using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Storage.Database.Entities;

public sealed class GroupEntity : EntityBase
{
	public required string GroupName { get; set; }
}
