using BookingAPI.DTOs;
using BookingAPI.Helpers;
using BookingAPI.Models;
using BookingAPI.Repositories.Interfaces;
using BookingAPI.Services.Interfaces;

namespace BookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IConfirmationRepository _confirmationRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<BookingService> _logger;

        public BookingService(
            IBookingRepository bookingRepository,
            IConfirmationRepository confirmationRepository,
            IEmailService emailService,
            ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _confirmationRepository = confirmationRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<BookingDTO> CreateAsync(CreateBookingDTO dto)
        {
            var booking = new Booking
            {
                FullName = dto.FullName,
                JobTitle = dto.JobTitle,
                CompanyName = dto.CompanyName,
                Email = dto.Email,
                Industry = dto.Industry,
                HelpWith = dto.HelpWith,
                ProblemDescription = dto.ProblemDescription,
                SessionGoal = dto.SessionGoal,
                Meeting = dto.Meeting,
                Date = dto.Date,
                Time = dto.Time,
                ContactPermission = dto.ContactPermission
            };

            booking = await _bookingRepository.CreateAsync(booking);

            var confirmation = new Confirmation
            {
                BookingId = booking.Id,
                Status = ConfirmationStatus.Pending
            };
            confirmation = await _confirmationRepository.CreateAsync(confirmation);

            await SendConfirmationAsync(booking, confirmation);

            return ToDto(booking);
        }

        public async Task<BookingDTO?> GetByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdBookingAsync(id);
            return booking is null ? null : ToDto(booking);
        }

        public async Task<PagedResult<BookingDTO>> GetAllAsync(int page, int pageSize)
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync(page, pageSize);
            var totalCount = await _bookingRepository.GetTotalCountAsync();

            return new PagedResult<BookingDTO>
            {
                Data = bookings.Select(ToDto),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        private async Task SendConfirmationAsync(Booking booking, Confirmation confirmation)
        {
            var confirmationDto = new ConfirmationDTO
            {
                Id = confirmation.Id,
                BookingId = booking.Id,
                FullName = booking.FullName,
                CompanyName = booking.CompanyName,
                HelpWith = booking.HelpWith,
                Meeting = booking.Meeting,
                BookingDate = booking.Date,
                BookingTime = booking.Time
            };

            try
            {
                await _emailService.SendBookingConfirmationAsync(confirmationDto, booking.Email);
                confirmation.Status = ConfirmationStatus.Sent;
                confirmation.SentAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send booking confirmation email for booking {BookingId}", booking.Id);
                confirmation.Status = ConfirmationStatus.Failed;
                confirmation.FailureReason = ex.Message;
            }

            await _confirmationRepository.UpdateAsync(confirmation);
        }

        private static BookingDTO ToDto(Booking booking) => new()
        {
            Id = booking.Id,
            FullName = booking.FullName,
            JobTitle = booking.JobTitle,
            CompanyName = booking.CompanyName,
            Email = booking.Email,
            Industry = booking.Industry,
            HelpWith = booking.HelpWith,
            ProblemDescription = booking.ProblemDescription,
            SessionGoal = booking.SessionGoal,
            Meeting = booking.Meeting,
            Date = booking.Date,
            Time = booking.Time,
            ContactPermission = booking.ContactPermission
        };
    }
}