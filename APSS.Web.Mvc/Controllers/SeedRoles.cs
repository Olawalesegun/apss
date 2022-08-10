using Microsoft.AspNetCore.Identity;
using APSS.Domain.Entities;

namespace APSS.Web.Mvc.Controllers;

public static class SeedRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Group.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Farmer.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Directorate.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.District.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Village.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Root.ToString()));
        await roleManager.CreateAsync(new IdentityRole(AccessLevel.Presedint.ToString()));
    }
}