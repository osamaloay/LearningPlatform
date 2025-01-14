using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplicationProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1); // Optionally set the idle timeout, if needed
    options.Cookie.HttpOnly = true; // Make the session cookie secure
    options.Cookie.IsEssential = true; // Make the session cookie essential for the app
});

// Add DbContext for database interaction
builder.Services.AddDbContext<WebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Use static files (e.g., for serving images, JS, CSS)
app.UseStaticFiles();

// Use session middleware
app.UseSession();  // This must come before app.UseRouting()

app.UseRouting();

// Use authorization (ensure the session is used correctly for user data)
app.UseAuthorization();
app.UseStaticFiles();

// Define default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); // Set default route to Login controller

app.Run();
