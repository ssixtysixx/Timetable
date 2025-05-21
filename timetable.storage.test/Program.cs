using Microsoft.EntityFrameworkCore;
using Timetable.Storage.Framework;

class Program
{
	public static async Task Main(string[] args)
	{
		var options = new DbContextOptionsBuilder<TimetableDBContext>();

		options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestAppDatabase;Trusted_Connection=False");

		var context = new TimetableDBContext(options.Options);

		var repo = new DisciplineRepository(context);
		
		var res = await repo.GetAllAsync(CancellationToken.None);
	}
}