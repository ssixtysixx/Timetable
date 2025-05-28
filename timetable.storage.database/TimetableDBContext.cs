using Microsoft.EntityFrameworkCore;
using Timetable.Storage.Database.Entities;

namespace Timetable.Storage.Framework;

public sealed class TimetableDBContext : DbContext
{
	public DbSet<GroupEntity> GroupsEntity { get; set; }

	public DbSet<TeacherEntity> TeachersEntity { get; set; }

	public DbSet<PlaceEntity> PlacesEntity { get; set; }

	public DbSet<DisciplineEntity> DisciplinesEntity { get; set; }

	public DbSet<TimetableRecordEntity> TimetableRecords { get; set; }

	public DbSet<UserAdminEntity> UserAdminEntities { get; set; }

	public TimetableDBContext(DbContextOptions<TimetableDBContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		//optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestAppDatabase;Trusted_Connection=False");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

    public void InitAdminUser()
    {
        var adminUser = UserAdminEntities.FirstOrDefault(u => u.Login == "123" && u.Password == "123");
        if (adminUser == null)
        {
            UserAdminEntities.Add(new UserAdminEntity { Login = "123", Password = "123" });
            SaveChanges();
        }
    }
}
