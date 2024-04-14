using yard.application.Services.Interface;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.application.Services.Implementation
{
	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IRoomTypeRepository _roomTypeRepository;

		public BookingService(IBookingRepository bookingRepository, IRoomTypeRepository roomTypeRepository)
		{
			_bookingRepository = bookingRepository;
			_roomTypeRepository = roomTypeRepository;
		}

		public async Task<BookingVM> AddBookingAsync(BookingVM booking)
		{
			BookingVM addedBooking = await _bookingRepository.AddBooking(booking);

			if (addedBooking == null)
			{
				throw new Exception("Booking Unsuccessful."); // AnyAsnc context.roo
			}

			return addedBooking;
		}

		public async Task<bool> RoomExistAsync(int roomId)
		{
			return await _roomTypeRepository.RoomExistsAsync(roomId);
		}

		public async Task<Room> GetRoomByIdAsync(int roomId)
		{
			return await _roomTypeRepository.GetRoomByIdAsync(roomId);
		}

		public async Task<bool> RoomTypeExistAsync(int roomTypeId)
		{
			return await _roomTypeRepository.RoomTypeExistsAsync(roomTypeId);
		}
	}
}