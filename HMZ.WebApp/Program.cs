using HMZ.Database.Data;
using HMZ.Database.Entities;
using HMZ.Service.Extensions;
using HMZ.Service.Services.PermissionServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHMZServices(builder.Configuration);
// route to lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors();

builder.Services.AddIdentityService(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region  Seed Data And Migrate
using var scope = builder.Services.BuildServiceProvider().CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<HMZContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    var permissionService = services.GetRequiredService<IPermissionService>();
    context.Database.Migrate();
    if (await Seed.SeedUser(userManager, roleManager, context) <= 0)
    {
        Console.WriteLine("No users seeded");
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}
#endregion

app.UseHttpsRedirection();


app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors(
    options => options.AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials() // for signalR
);
app.UseCookiePolicy();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Administrator",
    areaName: "Administrator",
    pattern: "Administrator/{controller=Dashboard}/{action=Index}/{id?}/{slug?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
