using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yard.application.Services.Interface;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomTypeRepository _roomTypeRepository;
        public BookingController(IBookingService bookingService, IRoomTypeRepository roomTypeRepository)
        {
            _bookingService = bookingService;
            _roomTypeRepository = roomTypeRepository;
        }

        [HttpPost("add-booking")]
        public async Task<ActionResult<BookingVM>> AddBookingAsync(BookingVM booking)
        {
            var newBooking = await _bookingService.AddBookingAsync(booking);
            return Ok(newBooking);
        }

    }
}
