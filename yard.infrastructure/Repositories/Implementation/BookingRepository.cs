using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure.Repositories.Implementation
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationContext _context;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public BookingRepository(ApplicationContext context, IHotelRepository hotelRepository, IMapper mapper)
        {
            _context = context;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<BookingVM> AddBooking(BookingVM booking)
        {
            // Check if hotel exists 
            var existingHotel = await _hotelRepository.GetHotelAsync(booking.HotelId);
            if (existingHotel == null)
            {
                throw new InvalidOperationException($"Hotel with ID {booking.HotelId} not found.");
            }

            // Check if check-in and check-out are valid
            if (booking.CheckIn.Date == booking.CheckOut.Date || booking.CheckOut.Date <= booking.CheckIn.AddHours(24).Date)
            {
                throw new InvalidOperationException("Invalid check-in and check-out dates. Check-out must be more than 24 hours after check-in.");
            }

            // Create Booking entity
            var bookingEntity = _mapper.Map<Booking>(booking);
            bookingEntity.BookingReference = GenerateBookingReference();

            // Add the booking to the repository
            _context.Bookings.Add(bookingEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingVM>(bookingEntity);
        }


        private string GenerateBookingReference()
        {
            var ticks = DateTime.Now.Ticks;
            var reference = $"Booking-{ticks}";

            return reference;
        }
    }
}
