using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Storage.Framework;

namespace Timetable.Storage.Database.Entities;

public sealed class PlaceEntity : EntityBase
{
	public PlaceType PlaceType { get; set; }

	public string PlaceName { get; set; }
}
