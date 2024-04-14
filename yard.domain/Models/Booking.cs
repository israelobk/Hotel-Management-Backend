using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
	public class Booking : BaseEntity
	{
		public int AppUserId { get; set; }
		public string BookingReference { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public int NoOfPeople { get; set; }
		public string ServiceName { get; set; }
		public Hotel Hotel { get; set; }
		public int HotelId { get; set; }
		public AppUser AppUser { get; set; }
		public bool PaymentStatus { get; set; }
		public int RoomId { get; set; } // were roomid is equal to request.roomid 
		public Room Room { get; set; }
		public Payment Payment { get; set; }
	}
}