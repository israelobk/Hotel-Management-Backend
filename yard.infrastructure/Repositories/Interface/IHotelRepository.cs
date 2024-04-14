using yard.domain.Models;
using yard.domain.ViewModels;

namespace yard.infrastructure.Repositories.Interface
{
	public interface IHotelRepository
	{
		Task<List<HotelVM>> GetAllHotels();

		Task<List<HotelVM>> GetAllHotels(string? hotelName, string? state, string? searchString);

		Task<HotelVM> SaveHotelAsync(HotelVM hotelVM);

		Task<HotelVM> GetHotelAsync(int id);

		Task<bool> HotelExistAsync(int hotelId);

		Task<List<RoomTypeVM>> GetRoomsInHotelAsync(int hotelId);

        Task<object> GetHotelWithRoomsAsync(int hotelId, bool includeRoomType);
        Task<bool> DelistHotelAsync(int hotelId);

    }
}