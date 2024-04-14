using System.ComponentModel.DataAnnotations;

namespace yard.domain.Models
{
    public class RoomType : BaseEntity
    {
        // public int HotelId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Thumbnail { get; set; }
        public Hotel Hotel { get; set; }
        public int BookedRooms 
        { 
            get { return Rooms.Count(room => room.IsBooked); } 
        }
        public int AvailableRooms
        {
            get { return Rooms?.Count - BookedRooms??0; }
        }
        public bool IsFullyBooked
        {
            get { return AvailableRooms == 0; }
        }

        public ICollection<Room> Rooms { get; set; }
    }
}
