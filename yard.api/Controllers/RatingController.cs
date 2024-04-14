using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yard.application.Services.Interface;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Implementation;
using yard.infrastructure.Repositories.Interface;

namespace yard.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingService _ratingService;
        private readonly IHotelService _hotelService;

        public RatingController(IRatingRepository ratingRepository, IRatingService ratingService, IHotelService hotelService)
        {
            _ratingRepository = ratingRepository;
            _ratingService = ratingService;
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<IActionResult> PostRating([FromBody] RatingVM ratingVM)
        {
            try
            {
                if (ratingVM == null)
                {
                    return BadRequest("Invalid rating data");
                }

                var addedRating = await _ratingRepository.AddRatingAsync(ratingVM);

                return CreatedAtAction(nameof(PostRating), new { id = addedRating.Id }, addedRating);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
            
        [HttpGet("get_ratings/{hotelId}")]
        public async Task<IActionResult> GetMyRatingsAsync(int hotelId)
        {
            var hotelexist = await _hotelService.HotelExistAsync(hotelId);
            if(hotelexist == false)
            {
            return NotFound($"Hotel Id { hotelId} does not exist");
            }
            return Ok(await _ratingService.GetRatingsAsync(hotelId));
        }

        
    }
}
