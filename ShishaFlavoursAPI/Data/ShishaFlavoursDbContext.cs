namespace ShishaFlavoursAPI.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
    using ShishaFlavoursAPI.Models;

    public class ShishaFlavoursDbContext : IdentityDbContext<User>
    {
        private DbContextOptions options;

        public ShishaFlavoursDbContext(DbContextOptions<ShishaFlavoursDbContext> options)
            : base(options)
        {
            this.options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var sqlServerOptions = options.GetExtension<SqlServerOptionsExtension>();
            optionsBuilder.UseSqlServer(sqlServerOptions.ConnectionString);

        }
    }
}
