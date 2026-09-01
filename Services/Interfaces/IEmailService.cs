using BookingAPI.DTOs;

namespace BookingAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(ConfirmationDTO confirmation, string recipientEmail);
    }
}