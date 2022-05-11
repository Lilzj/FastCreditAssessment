using FastCreditChallenge.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FastCreditChallenge.Data
{
    public class Seeder
    {
        private static string path = Path.GetFullPath(@"../FastCreditChallenge.Data/Data.Json/");

        private const string adminPassword = "Secret@123";
        private const string userPassword = "P@ssw0rd";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            //Get the Db context
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<FastCreditContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (context.Users.Any())
            {
                return;
            }
            else
            {
                //Get Usermanager and rolemanager from IoC container
                var userManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<UserManager<User>>();

                var roleManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //create role if it doesn't exists
                var roles = new string[] { "Admin", "Customer" };
                foreach (var role in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                //Seed Users with one Admin
                var Users = GetSampleData<User>(File.ReadAllText(path + "User.json"));

                var (adminCount, userCount) = (0, 0);

                foreach (var user in Users)
                {
                   if(adminCount < 1)
                    {
                        await userManager.CreateAsync(user, adminPassword);
                        await userManager.AddToRoleAsync(user, roles[0]);
                        ++adminCount;
                    }
                   else
                    {
                        await userManager.CreateAsync(user, userPassword);
                        await userManager.AddToRoleAsync(user, roles[1]);
                        ++userCount;
                    }
                }

                await context.SaveChangesAsync();
            }
        }
        private static List<T> GetSampleData<T>(string location)
        {
            var output = JsonSerializer.Deserialize<List<T>>(location, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }
    }
}
