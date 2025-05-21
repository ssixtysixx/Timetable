using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Timetable.Framework;
using Timetable.Storage.Framework;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();


		builder.Services.AddScoped(c =>
		{
			var optionsBuilder = new DbContextOptionsBuilder<TimetableDBContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestAppDatabase;Trusted_Connection=False");
			return optionsBuilder.Options;
		});

		builder.Services.AddScoped<TimetableDBContext>();

		builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();

		builder.Services.AddScoped<IGroupRepository, GroupRepository>();

		builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();

		builder.Services.AddScoped<IRecordRepository, RecordRepository>();

		builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if(!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();

		app.UseAuthorization();

		app.MapStaticAssets();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=raspis}/{action=Index}/{id?}")
			.WithStaticAssets();


		app.Run();
	}
}