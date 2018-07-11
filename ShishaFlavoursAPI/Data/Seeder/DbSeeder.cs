namespace ShishaFlavoursAPI.Data.Seeder
{
    public static class DbSeeder
    {
        public static void Seed(ShishaFlavoursDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
