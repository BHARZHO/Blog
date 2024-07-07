using AspNetCoreHero.ToastNotification.Extensions;
using AspNetCoreHero.ToastNotification;
using BlogApp.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogApplication.Data;
using BlogApplication.Models;
using BlogApplication.DAL.Contracts;
using BlogApplication.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(connectionString: connectionString, serverVersion).
        UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

        );
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<IBlogRepo, BlogRepo>().AddScoped<IAdmin, AdminRepo>().AddScoped<IAuthor, AuthorRepo>();
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();
builder.Services.AddMvc();
// builder.Services.AddTransient<ITask, TaskRepository>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
    options.SlidingExpiration = true;
});
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();