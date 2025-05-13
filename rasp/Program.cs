using Timetable.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();

builder.Services.AddScoped<IRecordRepository, RecordRepository>();

builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
