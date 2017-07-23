using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class LandlordContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public LandlordContext(DbContextOptions<LandlordContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(o => o.Id);
            builder.Entity<House>().HasKey(o => o.ID);
            builder.Entity<Mark>().HasKey(o => o.ID);
        }
    }
}
