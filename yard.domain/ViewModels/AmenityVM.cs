using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Models;

namespace yard.domain.ViewModels
{
    public class AmenityVM : BaseEntityVM
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
   

    }
}
