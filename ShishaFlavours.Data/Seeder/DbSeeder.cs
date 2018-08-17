namespace ShishaFlavoursAPI.Data.Seeder
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using ShishaFlavours.Models.Relationships;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavoursAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DbSeeder
    {
        public static void Seed(ShishaFlavoursDbContext context, UserManager<User> userManager, IFlavoursService flavoursService, IFlavourCombinationsService flavourCombinationsService, ILogger logger)
        {
            context.Database.EnsureCreated();

            CreateAdminUser(context, userManager, logger);
            CreateFlavours(context, flavoursService, logger);
            CreateFlavourCombinations(context, flavourCombinationsService, flavoursService, userManager, logger);
        }

        private static void CreateFlavours(ShishaFlavoursDbContext context, IFlavoursService flavoursService, ILogger logger)
        {
            if (!context.Flavours.Any())
            {
                flavoursService.AddFlavoursBulkAsync(GenerateFlavours().ToArray()).Wait();

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

        private static void CreateFlavourCombinations(ShishaFlavoursDbContext context, IFlavourCombinationsService flavourCombinationsService, IFlavoursService flavoursService, UserManager<User> userManager, ILogger logger)
        {
            if (!context.FlavourCombinations.Any())
            {
                string adminUserId = userManager.FindByNameAsync("Admin").Result.Id;

                FlavourCombination combination = new FlavourCombination()
                {
                    Name = "Sweet Mint",
                    Description = "A great combination of the most popular shisha flavours ever!",
                    DateAdded = DateTime.Now,
                    Votes = 20,
                    UserId = adminUserId,
                    FlavourCombinationReferences = new List<FlavourCombinationReference>()
                };

                List<Flavour> flavours = new List<Flavour>()
                {
                    flavoursService.GetFlavourByName("Blueberry").Result,
                    flavoursService.GetFlavourByName("Mint").Result
                };

                flavourCombinationsService.CreateFlavourCombinationAsync(combination, flavours).Wait();
            }
            else
            {
                logger.LogInformation("Flavour combinations were already added");
            }
        }

        private static List<Flavour> GenerateFlavours()
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

            return flavours;
        }
    }
}
