using Microsoft.EntityFrameworkCore;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework;

public sealed class TimetableDBContext : DbContext
{
	public DbSet<GroupEntity> Groups { get; set; }

	public DbSet<TeacherEntity> Teachers { get; set; }

	public DbSet<PlaceEntity> Places { get; set; }

	public DbSet<DisciplineEntity> Disciplines { get; set; }

	public DbSet<TimetableRecordEntity> TimetableRecords { get; set; }
}
