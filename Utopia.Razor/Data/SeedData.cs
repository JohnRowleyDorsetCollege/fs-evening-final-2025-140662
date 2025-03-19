using Bogus;
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


                string userName = "admin01@healthapp.com";
                if (await userManager.FindByEmailAsync(userName) == null)
                {
                    string password = "Admin@123";
                    var user = new IdentityUser { UserName = userName, Email = userName, EmailConfirmed = true };
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, HealthAppRoles.Admin);
                }

                var faker = new Faker();

                for (int i = 1; i <= 5; i++)
                {
                    string doctorName = $"dr-{i}-{faker.Name.FirstName()}";

                    string doctorEmail = faker.Internet.Email().ToLower();
                    Console.WriteLine(doctorEmail);
                    if (await userManager.FindByEmailAsync(doctorEmail) == null)
                    {
                        Console.WriteLine($"Email {doctorEmail} is being created");
                        string password = "Doctor@123";
                        var user = new IdentityUser { UserName = doctorName, Email = doctorEmail, EmailConfirmed = true };

                        if (user is null)
                        {
                            Console.WriteLine($"Email {doctorEmail} failed to create");
                        }
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, HealthAppRoles.Doctor);
                    }
                    else
                    {
                        Console.WriteLine($"Email {doctorEmail} exists");
                    }

                }

                for (int i = 1; i <= 5; i++)
                {

                    string patientName = $"pt-{i}-{faker.Name.FirstName()}";
                    string patientEmail = $"pat{i}.{faker.Internet.Email()}";
                    Console.WriteLine(patientEmail);
                    if (await userManager.FindByEmailAsync(patientEmail) == null)
                    {
                        string password = "Patient@123";
                        var user = new IdentityUser { UserName = patientName, Email = patientEmail, EmailConfirmed = true };
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, HealthAppRoles.Patient);
                    }

                }
            }

        }
    }
}
