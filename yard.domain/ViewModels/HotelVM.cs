using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Models;

namespace yard.domain.ViewModels
{
	public class HotelVM : BaseEntityVM
	{
		public HotelVM()
		{
			RoomTypes = new List<RoomTypeVM>();
			Ratings = new List<RatingVM>();
			Amenities = new List<AmenityVM>();
			WishLists = new List<WishListVM>();
			Bookings = new List<BookingVM>();
			Galleries = new List<GalleryVM>();
		}
		public string Name { get; set; }
		[DataType(DataType.Text)] public string Description { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		public AddressVM Address { get; set; }

		public ICollection<WishListVM> WishLists { get; set; }

		public ICollection<RatingVM> Ratings { get; set; }
		public ICollection<RoomTypeVM> RoomTypes { get; set; }
		public ICollection<AmenityVM> Amenities { get; set; }
		public ICollection<BookingVM> Bookings { get; set; }
		public ICollection<GalleryVM> Galleries { get; set; }
	}
}