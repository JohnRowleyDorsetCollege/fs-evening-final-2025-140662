﻿using Bogus;
using Microsoft.AspNetCore.Identity;

namespace Utopia.Razor.Data
{

    public static class HealthAppRoles
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
    }



    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {

                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new string[] { HealthAppRoles.Patient, HealthAppRoles.Doctor, HealthAppRoles.Admin };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        Console.WriteLine($"Creating role {role}");
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                string password = "Admin@123";
                string userName = "admin01@healthapp.com";
                if (await userManager.FindByEmailAsync(userName) == null)
                {

                    var user = new IdentityUser { UserName = userName, Email = userName, EmailConfirmed = true };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.Admin);
                }

                var faker = new Faker();

                for (int i = 0; i < 10; i++)
                {
                    string doctorName = faker.Name.FirstName();  
                    string doctorEmail = $"{doctorName}@healthapp.com";
                    Console.WriteLine(doctorEmail);
                    if (await userManager.FindByEmailAsync(doctorEmail) == null)
                    {
                        Console.WriteLine($"Email {doctorEmail} is being created");

                        var user = new IdentityUser { UserName = doctorEmail, Email = doctorEmail.ToLower(), EmailConfirmed = true };


                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, HealthAppRoles.Doctor);
                    }
                    else
                    {
                        Console.WriteLine($"Email {doctorEmail} exists");
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    string doctorName = faker.Name.FirstName();
                    string doctorEmail = $"pt-{doctorName}@healthappdr.com";
                    Console.WriteLine(doctorEmail);
                    if (await userManager.FindByEmailAsync(doctorEmail) == null)
                    {
                        Console.WriteLine($"Email {doctorEmail} is being created");

                        var user = new IdentityUser { UserName = doctorEmail, Email = doctorEmail.ToLower(), EmailConfirmed = true };


                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, HealthAppRoles.Patient);
                    }
                    else
                    {
                        Console.WriteLine($"Email {doctorEmail} exists");
                    }
                }



            }

        }
    }
}
