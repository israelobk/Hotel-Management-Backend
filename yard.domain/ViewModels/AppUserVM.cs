using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.enums;

namespace yard.domain.ViewModels
{
    public class AppUserVM : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AddressId { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<BookingVM> Bookings { get; set; }
        public ICollection<WishListVM> WishLists { get; set; }
        public ICollection<RatingVM> Ratings { get; set; }


    }
}
