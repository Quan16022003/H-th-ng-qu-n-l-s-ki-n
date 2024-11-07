using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Abtractions;
using Services;
using Persistence.Repositories;
using Web.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Domain.Entities;
using Web.Utils.ViewsPathServices;
using Web.Utils.ViewsPathServices.Implementations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MyDB");
builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity Service
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.SlidingExpiration = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationClientsideAdapters();

//builder.Services.AddValidatorsFromAssemblyContaining<OwnerForCreationInputModelValidator>(); // Nhớ mở ra để sử dụng FluentValidation

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

#region add path provider service for views in front end

// key: area name, value: service match
builder.Services.AddKeyedTransient<IPathProvider, AdminPathProvider>("Admin");
builder.Services.AddKeyedTransient<IPathProvider, ProfilePathProvider>("Profile");
builder.Services.AddKeyedTransient<IPathProvider, AccountPathProvider>("Account");

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

#region Area Route

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Profile",
    areaName: "Profile",
    pattern: "Profile/{controller}/{action}/{id?}");

app.MapAreaControllerRoute(
    name: "Account",
    areaName: "Account",
    pattern: "Account/{controller}/{action}/{id?}");

app.MapAreaControllerRoute(
    name: "Event",
    areaName: "Event",
    pattern: "Event/{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.MapRazorPages();

app.Run();
