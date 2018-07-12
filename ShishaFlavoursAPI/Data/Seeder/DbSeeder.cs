namespace ShishaFlavoursAPI.Data.Seeder
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using ShishaFlavoursAPI.Models;
    using ShishaFlavoursAPI.Services;
    using System.Collections.Generic;
    using System.Linq;

    public static class DbSeeder
    {
        public static void Seed(ShishaFlavoursDbContext context, UserManager<User> userManager, IFlavoursService flavoursService, ILogger logger)
        {
            context.Database.EnsureCreated();

            CreateAdminUser(context, userManager, logger);
            CreateFlavours(context, flavoursService, logger);
        }

        private static void CreateFlavours(ShishaFlavoursDbContext context, IFlavoursService flavoursService, ILogger logger)
        {
            if(!context.Flavours.Any())
            {
                List<Flavour> flavours = new List<Flavour>()
                {
                    new Flavour() { Name = "Double Apple" },
                    new Flavour() { Name = "Blueberry" },
                    new Flavour() { Name = "Pomegranate" },
                    new Flavour() { Name = "Watermelon" },
                    new Flavour() { Name = "Melon" },
                    new Flavour() { Name = "Mint" },
                    new Flavour() { Name = "Lemon" },
                    new Flavour() { Name = "Orange" },
                    new Flavour() { Name = "Grapefruit" },
                    new Flavour() { Name = "Grapes" }
                };

                foreach(var flavour in flavours)
                {
                    flavoursService.CreateFlavour(flavour.Name);
                }

                logger.LogInformation("Created 10 default shisha flavours");
            }
            else
            {
                logger.LogInformation("Flavours were already seeded");
            }
        }

        private static void CreateAdminUser(ShishaFlavoursDbContext context, UserManager<User> userManager, ILogger logger)
        {
            if (!context.Users.Any())
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
