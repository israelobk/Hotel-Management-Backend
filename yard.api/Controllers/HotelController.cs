using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using yard.application.Services.Interface;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelController : ControllerBase
	{
		private readonly IHotelService _hotelService;

		public HotelController(IHotelService hotelService, IRoomTypeRepository roomTypeRepository)
		{
			_hotelService = hotelService;
		}


		[HttpGet("get-all-hotels")]
		public async Task<IActionResult> GetAllHotels()
		{
			return Ok(await _hotelService.GetHotelsAsync());
		}

		[HttpGet("query-all-hotels")]
		// GET => api/Hotel/hotels?name=&state=&searchstring=
		public async Task<IActionResult> QueryAllHotels(
			[FromQuery(Name = "name")] string? name = null,
			[FromQuery(Name = "state")] string? state = null,
			[FromQuery(Name = "searchstring")] string? searchString = null
		)

		{
			return Ok(await _hotelService.GetHotelsAsync(name, state, searchString));
		}

		[HttpGet("{id}")]
		[ActionName("GetHotel")]
		public async Task<IActionResult> GetHotel(int id)
		{
			var hotel = await _hotelService.GetHotelAsync(id);

			if (hotel == null)
			{
				return NoContent();
			}

			return Ok(hotel);
		}

		[HttpPost]
		public async Task<IActionResult> SaveHotel(HotelVM hotelVM)
		{
			var hotelSaved = await _hotelService.SaveHotelAsync(hotelVM);

			if (hotelSaved != null)
			{
				return CreatedAtAction(nameof(GetHotel), new { id = hotelSaved.Id }, hotelSaved);
			}

			return Problem("Unable to be saved into the DB");
		}

		[HttpGet("{hotelId}/roomtypes")]
		public async Task<IActionResult> GetRoomsInHotelAsync(int hotelId)
		{
			var hotelExists = await _hotelService.HotelExistAsync(hotelId);

			if (!hotelExists)
			{
				return NotFound($"Hotel with Id {hotelId} not found.");
			}

			var roomTypes = await _hotelService.GetRoomsInHotelAsync(hotelId);

			return Ok(roomTypes);
		}

		[HttpGet("{hotelId}/GetHotel-Rooms")]
		public async Task<IActionResult> GetHotelWithRoomsAsync(int hotelId, [FromQuery] bool includeRoomType = false)
		{
			var hotelRoom = await _hotelService.GetHotelWithRoomsAsync(hotelId, includeRoomType);

			if (hotelRoom == null)
			{
				return NotFound($"Hotel with ID {hotelId} not found.");
			}

			return Ok(hotelRoom);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("delist-Hotel-by-id")]
		public async Task<IActionResult> DelistHotelAsync(int hotelId)
		{
			var delistedHotel = await _hotelService.DelistHotelAsync(hotelId);
			return Ok(delistedHotel);
		}
	}
}