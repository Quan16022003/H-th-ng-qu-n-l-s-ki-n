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
using Web.Areas.Admin.Validators.Owners;
using Web.Models;  // Import ApplicationUser

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<OwnerForCreationInputModelValidator>();

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var connectionString = builder.Configuration.GetConnectionString("MyDB");
builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<RepositoryDbContext>()
.AddDefaultTokenProviders();  

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm Authentication trước Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
