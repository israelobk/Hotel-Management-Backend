using yard.domain.ViewModels;

namespace yard.application.Services.Interface
{
    public interface IRatingService
    {
        Task<List<RatingVM>> GetRatingsAsync(int hotelId);
    }
}
