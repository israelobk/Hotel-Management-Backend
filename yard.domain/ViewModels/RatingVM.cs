using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.ViewModels
{
    public class RatingVM : BaseEntityVM
    {
        public int Ratings { get; set; }
        [DataType(DataType.Text)] 
        public string Comment { get; set; }
        public int HotelId { get; set; }
        public int AppUserId { get; set; }
     
    }
}
