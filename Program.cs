using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTable.Models.DataLayer;
using OpenTable.Models.DataLayer.Configuration;
using OpenTable.Models.DomainModels;

var builder = WebApplication.CreateBuilder(args);

// Add session services
builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure SQLite with Azure-safe path
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "OpenTable.sqlite");
builder.Services.AddDbContext<OpenTableContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<OpenTableContext>()
  .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OpenTableContext>();
    db.Database.EnsureCreated(); // Or db.Database.Migrate();

    await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}

// Set Azure dynamic port binding
//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//app.Urls.Add($"http://*:{port}");

app.MapStaticAssets();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.MapControllerRoute(
    name: "custom",
    pattern: "{controller}/{action}/metr-{activeMetropolis}/price-{activePriceRange}/cuisine={activeCuisine}");

app.Run();
