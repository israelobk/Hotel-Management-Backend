using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class Amenity : BaseEntity
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Hotel Hotel { get; set; }

    }
}
