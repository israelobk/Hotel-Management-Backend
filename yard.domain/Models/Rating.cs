using System.ComponentModel.DataAnnotations;

namespace yard.domain.Models
{
	public class Rating : BaseEntity
	{
		[Range(0, 10)] public int Ratings { get; set; }
		[DataType(DataType.Text)] public string Comment { get; set; }
		public int HotelId { get; set; }
		public int AppUserId { get; set; }
	}
}