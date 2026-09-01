using BookingAPI.Data;
using BookingAPI.Models;
using BookingAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingAPI.Repositories
{
    public class ConfirmationRepository : Repository<Confirmation>, IConfirmationRepository
    {
        public ConfirmationRepository(AppDbContext db) : base(db) {}

        public async Task<IEnumerable<Confirmation>> GetAllForBookingAsync(int bookingId) =>
            await _db.Confirmations
                .Where(c => c.BookingId == bookingId)
                .OrderByDescending(c => c.Booking.Date)
                .ToListAsync();
    }
}