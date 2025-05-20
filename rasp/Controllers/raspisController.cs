using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Timetable.Framework;
using Timetable.Framework.Records;
using System.Text.RegularExpressions;

public class RaspisController : Controller
{
	private readonly IDisciplineRepository _Disciplinerepository;
	private readonly IGroupRepository _grouprepository;
	private readonly IPlaceRepository _Placerepository;
	private readonly IRecordRepository _Recordrepository;
	private readonly ITeacherRepository _Teacherrepository;

	//TODO:
	public List<GroupDayRecord> Records { get; set; }

	public RaspisController(IDisciplineRepository Disciplinerepository, IGroupRepository Grouprepository, IPlaceRepository Placerepository, IRecordRepository Recordrepository, ITeacherRepository Teacherrepository)
	{
		_Disciplinerepository = Disciplinerepository;
		_grouprepository = Grouprepository;
		_Placerepository = Placerepository;
		_Recordrepository = Recordrepository;
		_Teacherrepository = Teacherrepository;
	}

    [HttpGet]
	public async Task<ActionResult> Index()
	{
		await ActivateAsync();
		return View(Records);
	}

	public async Task<ActionResult> ActivateAsync()
	{
		var group = new GroupRecord { Name = "0110" };

		var groupDayRecord = new GroupDayRecord
		{
			GroupRecord = group,
			Records =
			[
				new DayRecord 
				{ 
					Date = DateTime.Now, 
					SingleRecords = [
						new DaySingleRecord 
						{
							Number = 1,
							Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
							Group = group,
							Teacher = new TeacherRecord { Name = "FIO" },
							Place = new PlaceRecord { PlaceName = "place1", PlaceType = PlaceType.SportHall },
						},
						new DaySingleRecord
						{
							Number = 2,
							Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
							Group = group,
							Teacher = new TeacherRecord { Name = "FIO" },
							Place = new PlaceRecord { PlaceName = "place1", PlaceType = PlaceType.SportHall },
						},
				]},
				new DayRecord
				{
					Date = DateTime.Now,
					SingleRecords = [
						new DaySingleRecord
						{
							Number = 1,
							Discipline = new DisciplineRecord { DisciplineCode = "dis01" },
							Group = group,
							Teacher = new TeacherRecord { Name = "FIO" },
							Place = new PlaceRecord { PlaceName = "place1", PlaceType = PlaceType.SportHall },
						},
				]},
			]
		}; 

		var groupList = new List<GroupDayRecord>() { groupDayRecord };
        
		Records = groupList;

		return View(groupList);
	}
}