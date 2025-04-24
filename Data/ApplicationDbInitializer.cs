using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace NBD3.Data
{
    public static class ApplicationDbInitializer
    {
        static string[] roleNames = { "Admin", "Security", "Supervisor", "Designer", "General Manager", "Design Manager", "Group Manager", "Admin Assistant", "Sales Assoc", "Production Worker" };

        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                // Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                // Create Roles
                var RoleManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Create Users
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Define staff emails and their roles
                var staffEmailsAndRoles = new Dictionary<string, string>
                {
                    { "tbakken@nbd.ca", "Designer" },
                    { "sfenton@nbd.ca", "General Manager" },
                    { "kyamaguchi@nbd.ca", "Design Manager" },
                    { "cpoy@nbd.ca", "Group Manager" },
                    { "cnguyen@nbd.ca", "Admin Assistant" },
                    { "breinhardt@nbd.ca", "Sales Assoc" },
                    { "skaufman@nbd.ca", "Group Manager" },
                    { "mgoce@nbd.ca", "Production Worker" },
                    { "bswenson@nbd.ca", "Production Worker" },
                    { "designer1@nbd.ca", "Designer" },
                    { "designer2@nbd.ca", "Designer" }
                };

                // Add users based on staff emails with their respective roles
                foreach (var emailAndRole in staffEmailsAndRoles)
                {
                    AddUser(userManager, emailAndRole.Key, emailAndRole.Value);
                }

                AddUser(userManager, "admin@outlook.com", "Admin,Security", "Pa55w@rd");
                AddUser(userManager, "security@outlook.com", "Security", "Pa55w@rd");
                AddUser(userManager, "supervisor@outlook.com", "Supervisor", "Pa55w@rd");
                AddUser(userManager, "staff@outlook.com", "Staff", "Pa55w@rd");
                AddUser(userManager, "user@outlook.com");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
        /// <summary>
        /// Creates the Identity User and adds them to the roles.  
        /// Note that this sets EmailConfirmed to true.
        /// </summary>
        /// <param name="userManager">The UserManager<IdentityUser> </param>
        /// <param name="email">The email for the account.  Will also be used as the UserName</param>
        /// <param name="theRoles">String containing comma separated list of Role names. Omit if no roles</param>
        /// <param name="password">Password if you don't want the default</param>
        private static void AddUser(UserManager<IdentityUser> userManager,
            string email, string theRoles = "", string password = "Pa55w@rd")
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    string[] roles = theRoles.Split(',');
                    foreach (var role in roles)
                    {
                        if (roleNames.Contains(role))
                        {
                            userManager.AddToRoleAsync(user, role).Wait();
                        }
                    }
                }
            }
        }
    }
}
