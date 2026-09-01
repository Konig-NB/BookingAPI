using BookingAPI.DTOs;
using BookingAPI.Helpers;

namespace BookingAPI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDTO> CreateAsync(CreateBookingDTO dto);
        Task<BookingDTO?> GetByIdAsync(int id);
        Task<PagedResult<BookingDTO>> GetAllAsync(int page, int pageSize);
    }
}