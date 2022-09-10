using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

using APSS.Application.App;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Infrastructure.Repositores.EntityFramework;
using APSS.Infrastructure.Services;
using APSS.Web.Dtos.Profilies;
using APSS.Web.Mvc.Areas;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Util.Navigation.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddControllersWithViews();

#region Services

var svc = builder.Services;
// Database context
svc.AddDbContext<ApssDbContext>(cfg =>
    cfg.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);

// Unit-of-work
svc.AddScoped<IUnitOfWork, ApssUnitOfWork>();

// Services
svc.AddSingleton<IRandomGeneratorService, SecureRandomGeneratorService>();
svc.AddSingleton<ICryptoHashService, Argon2iCryptoHashService>();
svc.AddSingleton<IConfigurationService, AppSettingsConfigurationService>();
svc.AddScoped<IAuthService, AuthService>();
svc.AddScoped<ILogsService, DatabaseLogsService>();
svc.AddScoped<IUsersService, UsersService>();
svc.AddScoped<IPermissionsService, PermissionsService>();
svc.AddScoped<IAccountsService, AccountsService>();
svc.AddScoped<IAnimalService, AnimalService>();
svc.AddScoped<ILandService, LandService>();
svc.AddScoped<IPopulationService, PopulationService>();
svc.AddScoped<ISurveysService, SurveysService>();
svc.AddTransient<ISetupService, SetupService>();

// Auth service
svc.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    var settings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

    options.ExpireTimeSpan = settings.CookieExpiration;
    options.SlidingExpiration = settings.SlidingExpiration;

    options.AccessDeniedPath = "/Forbidden/";
    options.LoginPath = Routes.Auth.SignIn.FullPath;
    options.LogoutPath = Routes.Auth.SignOut.FullPath;
    options.ReturnUrlParameter = "returnUrl";

    options.EventsType = typeof(TokenValidationEvent);
});

svc.AddAuthorization();
svc.AddScoped<TokenValidationEvent>();

#endregion Services

var app = builder.Build();

// Environmen-dependent settings
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#region Routes

// Auth area
app.MapAreaControllerRoute(
    name: Areas.Auth,
    areaName: Areas.Auth,
    pattern: "{controller}/{action}");

// Setup area
app.MapAreaControllerRoute(
    name: Areas.Setup,
    areaName: Areas.Setup,
    pattern: "{controller}/{action=Index}");

foreach (var area in Areas.Dashboard)
{
    app.MapAreaControllerRoute(
        name: area,
        areaName: area,
        pattern: $"{{area:exists}}/{{controller={area}}}/{{action=Index}}/{{id?}}").RequireAuthorization();
}

// Redirects
var rewriteOpts = new RewriteOptions();
rewriteOpts.AddRedirect("^$", Areas.Home);

app.UseRewriter(rewriteOpts);

#endregion Routes

app.Run();