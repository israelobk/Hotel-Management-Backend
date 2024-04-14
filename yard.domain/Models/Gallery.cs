using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class Gallery : BaseEntity
    {
        public int HotelId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeature { get; set; }
        public Hotel Hotel { get; set; }


    }
}
