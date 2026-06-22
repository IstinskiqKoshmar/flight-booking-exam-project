using FlightBooking.Data;
using FlightBooking.Data.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FlightBookingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys")));

var app = builder.Build();
Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "App_Data", "Keys"));
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FlightBookingDbContext>();
    db.Database.EnsureCreated();
}
app.Run();

public partial class Program { }
