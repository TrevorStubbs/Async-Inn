using AsyncInn.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class RoleInitializer
    {
        // create a list of identity roles
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.DistrictManager, NormalizedName = ApplicationRoles.DistrictManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.PropertyManager, NormalizedName = ApplicationRoles.PropertyManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.Agent, NormalizedName = ApplicationRoles.Agent.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()}

        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> users, IConfiguration _config) // add 2 things here
        {
            using (var dbContext = new AsyncInnDbContext(serviceProvider.GetRequiredService<DbContextOptions<AsyncInnDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                // Add a thing
                SeedUsers(users, _config);
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration _config) // add a thing
        {
            if (userManager.FindByEmailAsync(_config["AdminEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["AdminEmail"];
                user.Email = _config["AdminEmail"];
                user.FirstName = "Trevor";
                user.LastName = "Stubbs";

                IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.DistrictManager).Wait();
                }

            }
        }

        private static void AddRoles(AsyncInnDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }




        // method creates/checks the db

        // add the roles to the db directly
    }
}
