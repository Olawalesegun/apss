using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using APSS.Application.App;
using APSS.Domain.Entities;
using APSS.Domain.Repositories;
using APSS.Domain.Services;
using APSS.Infrastructure.Repositores.EntityFramework;
using APSS.Infrastructure.Services;
using APSS.Web.Mvc.Auth;
using APSS.Web.Mvc.Auth.Handlers;
using APSS.Web.Mvc.Auth.Requirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
svc.AddScoped<IAuthService, JwtAuthService>();
svc.AddScoped<ILogsService, DatabaseLogsService>();
svc.AddScoped<IUsersService, UsersService>();
svc.AddScoped<IPermissionsService, PermissionsService>();
svc.AddScoped<IAccountsService, AccountsService>();
svc.AddScoped<IAnimalService, AnimalService>();
svc.AddScoped<ILandService, LandService>();
svc.AddScoped<IPopulationService, PopulationService>();
svc.AddScoped<ISurveysService, SurveysService>();

// Auth service
svc.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    var settings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

    options.ExpireTimeSpan = settings.CookieExpiration;
    options.SlidingExpiration = settings.SlidingExpiration;
    options.AccessDeniedPath = "/Forbidden/";
    options.EventsType = typeof(TokenValidationEvent);
});

// Authorization
svc.AddSingleton<IAuthorizationHandler, ShouldHaveCreatePermissionRequirementHandler>();
svc.AddSingleton<IAuthorizationHandler, ShouldHaveReadPermissionRequirementHandler>();
svc.AddSingleton<IAuthorizationHandler, ShouldHaveUpdatePermissionRequirementHandler>();
svc.AddSingleton<IAuthorizationHandler, ShouldHaveDeletePermissionRequirementHandler>();

svc.AddAuthorization(options =>
{
    var registerPermissionPolicy = (PermissionType name, IAuthorizationRequirement requirement) =>
    {
        options.AddPolicy(name.GetPermissionValues().First(), policy =>
        {
            policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser();
            policy.RequireClaim(ClaimTypes.Role);
            policy.Requirements.Add(requirement);
        });
    };

    // policies
    registerPermissionPolicy(PermissionType.Create, new ShouldHaveCreatePermissionRequirement());
    registerPermissionPolicy(PermissionType.Read, new ShouldHaveReadPermissionRequirement());
    registerPermissionPolicy(PermissionType.Update, new ShouldHaveUpdatePermissionRequirement());
    registerPermissionPolicy(PermissionType.Delete, new ShouldHaveDeletePermissionRequirement());
});

builder.Services.AddScoped<TokenValidationEvent>();

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

app.UseAuthorization();
app.UseAuthentication();

// Cookie policies
app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always,
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Family}/{action=GetFamilies}/{id?}");

app.Run();