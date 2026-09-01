using Microsoft.EntityFrameworkCore;
using BookingAPI.Models;

namespace BookingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Confirmation> Confirmations => Set<Confirmation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking -> Confirmation (one-to-one)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Confirmation)
                .WithOne(c => c.Booking)
                .HasForeignKey<Confirmation>(c => c.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}