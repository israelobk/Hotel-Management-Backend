using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.ViewModels
{
    public class RoomVM : BaseEntityVM
    {
        public int RoomTypeId { get; set; }
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }
      
    }

}

