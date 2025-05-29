using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using rasp;

using System.Threading.Tasks;

using TechTeaStudio.Config;

using Timetable.Framework;
using Timetable.Storage.Database;
using Timetable.Storage.Database.Repositories;
using Timetable.Storage.Framework;
using Timetable.Storage.Framework.Repositories;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/raspis";
                options.AccessDeniedPath = "/raspis";
            });

        //Где там моя базированная конфигурация
        var configHandler = new ConfigFileHandler<AppConfig>(
        directoryPath: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CGU"),
        fileName: "CoolestConfigForCGURaspisanieSite",
        fileExtension: "json",
        defaultConfig: new AppConfig(
                connectionStringId: "Никита",
                connectionStrings:
                [
                    new("Никита", "Server=(localdb)\\MSSQLLocalDB;Database=TestAppDatabase;Trusted_Connection=False"),
                    new("CEO", @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = NikitaBase; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False"),
                ]
        ),
            new JsonConfigSerializer<AppConfig>()
        );

        var config = configHandler.ReadConfig();
        var connectionString = config.ConnectionStrings.Find(x => x.Name == config.ConnectionStringId);


        builder.Services.AddScoped(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<TimetableDBContext>();
            optionsBuilder.UseSqlServer(connectionString?.Value);
            return optionsBuilder.Options;
        });

        builder.Services.AddScoped<TimetableDBContext>();

        builder.Services.AddScoped<IContextFactory, ContextFactory>();

        builder.Services.AddScoped<IRecordRepository, RecordRepository>();
        builder.Services.AddScoped<RecordRepository, RecordRepository>();
        builder.Services.AddScoped<IRecordMutationRepository, RecordRepository>();


        builder.Services.AddScoped<IUserRepository, UserRepository>();
        var app = builder.Build();

        var services = app.Services.CreateScope();
        var context = services.ServiceProvider.GetRequiredService<TimetableDBContext>();
        var repository = services.ServiceProvider.GetRequiredService<RecordRepository>();
        //await repository.SeedTestData();
        context.InitAdminUser();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication(); 
        app.UseAuthorization();
        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=raspis}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}