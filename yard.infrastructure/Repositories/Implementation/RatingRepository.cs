using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure.Repositories.Implementation
{

    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RatingRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RatingVM> AddRatingAsync(RatingVM ratingVM)
        {
            var rating = _mapper.Map<Rating>(ratingVM); 
            _context.Ratings.Add(rating);
            int res = await _context.SaveChangesAsync();

            if(res > 0)
            {
                ratingVM.Id = rating.Id;
                return ratingVM;
            }
            return null;
        }
        public async Task<List<RatingVM>> GetRatingsAsync(int hotelId)
        {
            var RatingsFromDb = await _context.Ratings
            .Where(a => a.HotelId == hotelId).ToListAsync();
            
            List<RatingVM> ViewRatings = _mapper.Map<List<RatingVM>>(RatingsFromDb);
            return ViewRatings;
        }
           

        
                
           

        

        //public Task AddRatingAsync(Rating rating)
        //{
        //    throw new NotImplementedException();
        //}
    }
    
}
