using Constracts;
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
using Microsoft.AspNetCore.Mvc;
using Web.Config;
using Web.Utils;
using Mapster;

using EmailService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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

builder.Services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.SlidingExpiration = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = "1074471028840-piqqp4vsomjt1qs16j1h522f472v00pn.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.Scope.Add("email");
                options.Scope.Add("profile");
            })
            .AddFacebook(options =>
            {
                options.AppId = "579921444620523";
                options.AppSecret = "3c1b92b40cc492da5b0ef8b25bd29011";
                options.Scope.Add("email");
                options.Scope.Add("public_profile");
            });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidationClientsideAdapters();

//builder.Services.AddValidatorsFromAssemblyContaining<OwnerForCreationInputModelValidator>(); // Nhớ mở ra để sử dụng FluentValidation

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add repositories to DI container
builder.Services.RegisterAllRepositories();
builder.Services.RegisterAllServices();
builder.Services.RegisterPolicy();

builder.Services.AddHostedService<OrderCancelledService>();

builder.Services.RegisterPathProvideManager();

builder.Services.RegisterSlugifyTransformer();

builder.Services.AddScoped<IFileService>(provider => 
    new FileService(builder.Environment.WebRootPath));

builder.SetEnvRootPath();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
if (emailConfig == null)
{
    throw new ArgumentNullException(nameof(emailConfig), "Email configuration cannot be null.");
}

builder.Services.AddSingleton(emailConfig);
WebApplication app = builder.Build();

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

app.RegisterAllRoutes();
app.MapRazorPages();

TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);
EnvLoader.Load(".env");

app.Run();
