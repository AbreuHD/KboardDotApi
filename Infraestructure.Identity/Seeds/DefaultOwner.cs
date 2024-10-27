using Core.Application.Entities;
using Core.Application.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Identity.Seeds
{
    public static class DefaultOwner
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new ApplicationUser
            {
                Name = "John",
                LastName = "Doe",
                UserName = "JohnDoe",
                Email = "JohnDoe@JohnDoe.com",
                PhoneNumber = "809-666-6666",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(user.Email);
                if (userEmail == null)
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    await userManager.AddToRoleAsync(user, Roles.Owner.ToString());
                }
            }
        }
    }
}
