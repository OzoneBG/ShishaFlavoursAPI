namespace ShishaFlavoursAPI.Common.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ShishaFlavoursAPI.Data;
    using ShishaFlavoursAPI.Data.Seeder;
    using ShishaFlavoursAPI.Models;

    public static class DbSeederExtension
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var context = services.GetRequiredService<ShishaFlavoursDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();

                DbSeeder.Seed(context, userManager, logger);
            }

            return app;
        }
    }
}
