using Microsoft.EntityFrameworkCore;
using VisitorManagementSystem.Models.Entities;


namespace VisitorManagementSystem.Models
{
    public class VisitorDbContext : DbContext
    {
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ADLogin> ADLogins { get; set; }

 
        public VisitorDbContext(DbContextOptions<VisitorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ADLogin>().HasNoKey();
        }

    }
}
