using BookingAPI.Models;

namespace BookingAPI.Repositories.Interfaces
{
    public interface IConfirmationRepository : IRepository<Confirmation>
    {
        Task<IEnumerable<Confirmation>> GetAllForBookingAsync(int bookingId);
    }
}