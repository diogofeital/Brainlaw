using DesafioBrainlaw.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DesafioBrainlaw.Infrastructure.Context
{
    public sealed class DesafioBrainlawContext : DbContext
    {
        public DesafioBrainlawContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}