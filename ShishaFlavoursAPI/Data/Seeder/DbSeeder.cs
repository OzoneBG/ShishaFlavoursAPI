namespace ShishaFlavoursAPI.Data.Seeder
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using ShishaFlavoursAPI.Models;
    using System.Linq;

    public static class DbSeeder
    {
        public static void Seed(ShishaFlavoursDbContext context, UserManager<User> userManager, ILogger logger)
        {
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                User user = new User
                {
                    UserName = "Admin",
                    Email = "muzunov@hotmail.com"
                };

                string password = "1q2w3e$R";
                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    logger.LogInformation("Successfully created 'admin' user");
                }
                else
                {
                    logger.LogError("Failed to create the 'admin' user");
                    logger.LogError(result.Errors.ToString());
                }
            }
            else
            {
                logger.LogInformation("Admin user already exists");
            }
        }
    }
}
