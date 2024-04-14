using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.ViewModels
{
    public class BookingVM : BaseEntityVM
    {
        public int AppUserId { get; set; }
        public int HotelId { get; set; }
        public string BookingReference { get; set; } //Guid.New Guid()
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NoOfPeople { get; set; }
        public string ServiceName { get; set; }
        public bool PaymentStatus { get; set; } //true
        public int RoomId { get; set; }

        public int AvailableRooms { get; set; }

    }
}
