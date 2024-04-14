using System.ComponentModel.DataAnnotations;

namespace yard.domain.ViewModels
{
    public class RoomTypeVM : BaseEntityVM
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public decimal Price { get; set; }


        //public decimal Discount { get; set; }
       // public string Thumbnail { get; set; }
        //public ICollection<RoomVM> Rooms { get; set; }
    }
}
