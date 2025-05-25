using Microsoft.EntityFrameworkCore;
using Timetable.Storage.Framework;

class Program
{
	public static async Task Main(string[] args)
	{
		var options = new DbContextOptionsBuilder<TimetableDBContext>();

		options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = NikitaBase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False");

		var context = new TimetableDBContext(options.Options);
		context.Database.EnsureCreated();
		//var repo = new DisciplineRepository(context);
		
		//var res = await repo.GetAllAsync(CancellationToken.None);
	}
}