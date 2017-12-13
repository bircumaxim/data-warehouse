using Banking.Data.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data
{
    public class DbApplicationContext : DbContext
    {
        protected DbApplicationContext()
        {
            Database.Migrate();
        }

        public DbApplicationContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transaction>();
            modelBuilder.Entity<Payment>().HasOne(t => t.Transaction);
            modelBuilder.Entity<Client>().HasMany(p => p.Payments);
        }
    }
}