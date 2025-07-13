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

// Register ApplicationDbContext
builder.Services.AddDbContext<OpenTableContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OpenTableContext")));
builder.Services.AddIdentity<User, IdentityRole>(options => {
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
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}
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
