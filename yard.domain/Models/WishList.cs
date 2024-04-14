using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class WishList : BaseEntity
    {
        public int AppUserId { get; set; }
        public int HotelId { get; set; }
        public AppUser AppUser { get; set; }
        public Hotel Hotel { get; set; }

    }
}
