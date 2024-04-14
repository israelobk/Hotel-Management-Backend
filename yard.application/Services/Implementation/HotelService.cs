using Microsoft.EntityFrameworkCore;
using yard.application.Services.Interface;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.application.Services.Implementation
{
	public class HotelService : IHotelService
	{
		private readonly IHotelRepository _hotelRepository;

		public HotelService(IHotelRepository hotelRepository)
		{
			_hotelRepository = hotelRepository;
		}

		public async Task<HotelVM> GetHotelAsync(int id)
		{
			return await _hotelRepository.GetHotelAsync(id);
		}

		public async Task<List<HotelVM>> GetHotelsAsync()
		{
			var hotels = await _hotelRepository.GetAllHotels();

			return hotels;
		}

		public async Task<HotelVM> SaveHotelAsync(HotelVM hotelVM)
		{
			HotelVM hotel = await _hotelRepository.SaveHotelAsync(hotelVM);

			return hotel;
		}


		public async Task<List<HotelVM>> GetHotelsAsync(string name, string state, string searchString)
		{
			var hotels = await _hotelRepository.GetAllHotels(name, state, searchString);

			return hotels;
		}

		public async Task<bool> HotelExistAsync(int hotelId)
		{
			return await _hotelRepository.HotelExistAsync(hotelId);
		}

		public async Task<List<RoomTypeVM>> GetRoomsInHotelAsync(int hotelId)
		{
			return await _hotelRepository.GetRoomsInHotelAsync(hotelId);
		}

		public async Task<object> GetHotelWithRoomsAsync(int hotelId, bool includeRoomType)
		{
			return await _hotelRepository.GetHotelWithRoomsAsync(hotelId, includeRoomType);

        }
        public async Task<bool> DelistHotelAsync(int hotelId)
        {
            return await _hotelRepository.DelistHotelAsync(hotelId);
        }
        
    }
}