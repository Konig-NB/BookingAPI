using BookingAPI.Models;

namespace BookingAPI.Repositories.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Booking?> GetByIdBookingAsync(int id);
    }
}