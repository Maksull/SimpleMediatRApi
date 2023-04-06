using Microsoft.EntityFrameworkCore;
using SimpleMediatr.Data.Configurations;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Database
{
    public class SimpleMediatrDataContext : DbContext
    {
        public SimpleMediatrDataContext(DbContextOptions<SimpleMediatrDataContext> opts) : base(opts) { }

        public SimpleMediatrDataContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
