using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.dto;
using yard.domain.Models;
using yard.domain.ViewModels;

namespace yard.infrastructure.Repositories.Interface
{
    public interface IRatingRepository
    {
        Task<RatingVM> AddRatingAsync(RatingVM ratingVM);
        //Task AddRatingAsync(Rating rating);
        Task<List<RatingVM>> GetRatingsAsync(int hotelId);
    }
}
