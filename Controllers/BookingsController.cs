using BookingAPI.DTOs;
using BookingAPI.Helpers;
using BookingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [EnableRateLimiting("booking")]
        public async Task<ActionResult<BookingDTO>> Create([FromBody] CreateBookingDTO dto)
        {
            var booking = await _bookingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookingDTO>> GetById(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            return booking is null ? NotFound() : Ok(booking);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BookingDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _bookingService.GetAllAsync(page, pageSize);
            return Ok(result);
        }
    }
}