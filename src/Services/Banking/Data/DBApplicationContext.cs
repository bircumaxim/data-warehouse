using Microsoft.EntityFrameworkCore;

namespace Banking.Data
{
    public class DbApplicationContext : DbContext
    {
        protected DbApplicationContext()
        {
//            Database.Migrate();
        }

        public DbApplicationContext(DbContextOptions options) : base(options)
        {
//            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}