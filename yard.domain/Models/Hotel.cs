using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class Hotel : BaseEntity
    {
        public Hotel()
        {
        }

        public string Name { get; set; }
        [DataType(DataType.Text)] 
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    
        public ICollection<WishList> WishLists { get; set; }
      
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<RoomType> RoomTypes { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Gallery> Galleries { get; set; }
    }
}
