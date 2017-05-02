using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class LandlordContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public LandlordContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
