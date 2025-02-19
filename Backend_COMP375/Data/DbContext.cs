using Backend_COMP375.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_COMP375.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optyions) : base(optyions) { }

        public DbSet<User> Users{ get; set; }
        public DbSet<Club> Clubs{ get; set; }
        public DbSet<Order> Orders{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Club)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
