using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.ViewModels
{
    public class WishListVM : BaseEntityVM
    {
        public int AppUserId { get; set; }
        public int HotelId { get; set; }

    }
}
