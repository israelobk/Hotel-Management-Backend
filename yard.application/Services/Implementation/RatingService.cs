using yard.application.Services.Interface;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.application.Services.Implementation
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
         }
        public async Task<List<RatingVM>> GetRatingsAsync(int hotelId)
        {
           return  await _ratingRepository.GetRatingsAsync(hotelId);
            
        }
    }
}
