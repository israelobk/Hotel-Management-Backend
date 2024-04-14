using AutoMapper;
using Microsoft.EntityFrameworkCore;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure.Repositories.Implementation
{
	public class HotelRepository : IHotelRepository
	{
		private readonly ApplicationContext _context;
		private readonly IMapper _mapper;

		public HotelRepository(ApplicationContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<List<HotelVM>> GetAllHotels()
		{
			List<Hotel> hotelsFromDb =
				await _context.Hotels.Include(x => x.Address).Include(x => x.RoomTypes).ToListAsync();

			List<HotelVM> hotelsToReturn = _mapper.Map<List<HotelVM>>(hotelsFromDb);

			return hotelsToReturn;
		}

		public async Task<HotelVM> GetHotelAsync(int id)
		{
			var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);

			var wishLists = hotel.WishLists;
			var hotelToReturn = _mapper.Map<HotelVM>(hotel);

			return hotelToReturn;
		}

		public async Task<HotelVM> SaveHotelAsync(HotelVM hotelVM)
		{
			var hotelToSave = _mapper.Map<Hotel>(hotelVM);

			_context.Hotels.Add(hotelToSave);

			var result = await _context.SaveChangesAsync();

			if (result <= 0) return null;

			hotelVM.Id = hotelToSave.Id;

			return hotelVM;
		}

		public async Task<List<HotelVM>> GetAllHotels(string? hotelName, string? state, string? searchString)
		{
			//IF SEARCH AND FILTER ARE EMPTY
			if (hotelName == null && state == null && searchString == null)
			{
				return await GetAllHotels();
			}

			IQueryable<Hotel> hotelsFromDB = _context.Hotels;

			//IF SEARCH IS EMPTY
			if (searchString == null)
			{
				var hotels = FilterLogic(hotelName, state, hotelsFromDB);

				var hotelsToReturn = _mapper.Map<List<HotelVM>>(hotels);

				return hotelsToReturn;
			}

			//IF FILTER VALUES ARE EMPTY
			if (hotelName == null && state == null)
			{
				var hotelsDB = SearchLogic(searchString, hotelsFromDB);

				var hotelsToReturn = _mapper.Map<List<HotelVM>>(hotelsDB);

				return hotelsToReturn;
			}

			var hotelsFromFilter = FilterLogic(hotelName, state, hotelsFromDB);

			var hotelsFromSearch = SearchLogic(searchString, hotelsFromFilter);

			var hotelsReturn = _mapper.Map<List<HotelVM>>(hotelsFromSearch);

			return hotelsReturn;
		}

		private IQueryable<Hotel> FilterLogic(string hotelName, string state,
			IQueryable<Hotel> hotelsFromDB)
		{
			if (hotelName == null && state != null)
			{
				var hotels = hotelsFromDB.Where(h => h.Address.State.Contains(state));
				return hotels;
			}

			if (hotelName != null && state == null)
			{
				var hotels = hotelsFromDB.Where(h => h.Name.Contains(hotelName));
				return hotels;
			}

			var hotelsToReturn = hotelsFromDB.Where(h => h.Address.State.Contains(state) && h.Name.Contains(hotelName));

			return hotelsToReturn;
		}

		private IQueryable<Hotel> SearchLogic(string searchString, IQueryable<Hotel> hotelsFromDB)
		{
			var hotels = hotelsFromDB.Where(h =>
				h.Name.Contains(searchString) || h.Address.Street.Contains(searchString) ||
				h.Address.State.Contains(searchString) || h.Address.Country.Contains(searchString) ||
				h.Address.City.Contains(searchString) || h.Description.Contains(searchString));

			return hotels;
		}

		public async Task<bool> HotelExistAsync(int hotelId)
		{
			return await _context.Hotels.AnyAsync(h => h.Id == hotelId);
		}

		public async Task<List<RoomTypeVM>> GetRoomsInHotelAsync(int hotelId)
		{
			var roomTypesFromDb = await _context.RoomTypes.Include(x => x.Hotel)
				.Where(h => h.Hotel.Id == hotelId)
				.ToListAsync();

			var roomsToReturn = _mapper.Map<List<RoomTypeVM>>(roomTypesFromDb);

			return roomsToReturn;


			//fourthlayer - talks directly to thirdlayer -- Client (Postman, Swagger, Browser, React, Angular, Vue, MVC, Fiddle)
			//thirdlayer - talks directly to secondlayer -- Controller
			//secondlayer - talks directly to firstlayer -- Service
			//firstlayer - talks directly to the DB -- Repository

			//DB
		}


		public async Task<object> GetHotelWithRoomsAsync(int hotelId, bool includeRoomType)
		{
			var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);

			var hotelToReturn = _mapper.Map<HotelVM>(hotel);

			if (includeRoomType)
			{
				var roomTypesFromDb = await _context.RoomTypes.Include(x => x.Hotel)
					.Where(h => h.Hotel.Id == hotelId)
					.ToListAsync();

				var roomTypesToReturn = _mapper.Map<List<RoomTypeVM>>(roomTypesFromDb);
				return new { Hotel = hotelToReturn, RoomTypes = roomTypesToReturn };
			}

			return hotelToReturn;
		}

		public async Task<bool> HotelExistsAsync(int hotelId)
		{
			return await _context.Hotels.AnyAsync(h => h.Id == hotelId);
		}

		public async Task<bool> DelistHotelAsync(int hotelId)
		{
			var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);

			hotel.IsDelisted = true;

			return true;
		}
	}
}