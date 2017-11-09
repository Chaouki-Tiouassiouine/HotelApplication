using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApplication.Data
{
    public static class Seeder
    {
        public static void Initialize(this IApplicationBuilder app, IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            // check for roles 
            var managerRole = roleManager.FindByNameAsync("Hotelmanager").Result;
            if (managerRole == null)
            {
                managerRole = new IdentityRole("Hotelmanager");
                roleManager.CreateAsync(managerRole).Wait();
            }
            var guestRole = roleManager.FindByNameAsync("Guest").Result;
            if (guestRole == null)
            {
                guestRole = new IdentityRole("Guest");
                roleManager.CreateAsync(guestRole).Wait();
            }
            var recepRole = roleManager.FindByNameAsync("Receptionist").Result;
            if (recepRole == null)
            {
                recepRole = new IdentityRole("Receptionist");
                roleManager.CreateAsync(recepRole).Wait();
            }

            var adminUser = userManager.FindByNameAsync("Ramon@delgado.nl").Result;
            if (adminUser == null)
            {
                adminUser = new ApplicationUser() { UserName = "Ramon@delgado.nl", Email = "Ramon@delgado.nl", FirstName = "Ramon", LastName = "Delgado" };
                var result = userManager.CreateAsync(adminUser, "Wachtwoord1!").Result;
                result = userManager.SetLockoutEnabledAsync(adminUser, false).Result;

                userManager.AddToRoleAsync(adminUser, managerRole.Name).Wait();
                userManager.AddToRoleAsync(adminUser, recepRole.Name).Wait();
            }
        }
    }
}