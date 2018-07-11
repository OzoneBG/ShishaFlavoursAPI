namespace ShishaFlavoursAPI.Common.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using ShishaFlavoursAPI.Data;
    using ShishaFlavoursAPI.Data.Seeder;

    public static class DbSeederExtension
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var context = services.GetRequiredService<ShishaFlavoursDbContext>();

                DbSeeder.Seed(context);
            }

            return app;
        }
    }
}
