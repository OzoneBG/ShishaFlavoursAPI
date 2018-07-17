namespace ShishaFlavoursAPI.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
    using ShishaFlavours.Models.Relationships;
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

        public DbSet<Flavour> Flavours { get; set; }

        public DbSet<FlavourCombination> FlavourCombinations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlavourCombinationReference>()
                .HasKey(c => new { c.FlavourId, c.FlavourCombinationId });

            builder.Entity<FlavourCombinationReference>()
                .HasOne(f => f.FlavourCombination)
                .WithMany(f => f.FlavourCombinationReferences)
                .HasForeignKey(f => f.FlavourCombinationId);

            builder.Entity<FlavourCombinationReference>()
                .HasOne(f => f.Flavour)
                .WithMany(f => f.FlavourCombinationReferences)
                .HasForeignKey(f => f.FlavourId);
        }
    }
}
