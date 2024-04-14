using Microsoft.AspNetCore.Mvc;
using yard.application.Services.Interface;

namespace yard.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomController : ControllerBase
	{
		private readonly IRoomService _roomService;

		public RoomController(IRoomService roomService)
		{
			_roomService = roomService;
		}

		[HttpGet("available-rooms/{roomTypeId}")]
		public async Task<ActionResult<int>> GetAvailableRooms(int hotelId, int roomTypeId)
		{
			try
			{
				int availableRooms = await _roomService.GetAvailableRooms(hotelId, roomTypeId);
				return Ok(availableRooms);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("get-room-type/{hotelId}/{roomtypeId}")]
		public IActionResult GetRoom(int hotelId, int roomtypeId)
		{
			var result = _roomService.GetRoom(hotelId, roomtypeId);

			if (result.RoomType != null)
			{
				return Ok(result.RoomType);
			}

			return NotFound(result.ErrorMessage);
		}
	}
}