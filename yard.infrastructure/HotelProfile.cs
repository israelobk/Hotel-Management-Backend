using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using yard.domain.Models;
using yard.domain.ViewModels;

using ModelFromDB = yard.domain.Models;

namespace yard.infrastructure
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            //TO //GETTING FROM THE DB
            CreateMap<ModelFromDB.Address, AddressVM>();
            CreateMap<ModelFromDB.Amenity, AmenityVM>();
            CreateMap<ModelFromDB.AppUser, AppUserVM>();
            CreateMap<ModelFromDB.BaseEntity, BaseEntityVM>();
            CreateMap<ModelFromDB.Booking, BookingVM>();
            CreateMap<ModelFromDB.Gallery, GalleryVM>();
            CreateMap<ModelFromDB.Hotel, HotelVM>();
            CreateMap<ModelFromDB.Payment, PaymentVM>();
            CreateMap<ModelFromDB.Rating, RatingVM>();
            CreateMap<ModelFromDB.Room, RoomVM>();
            CreateMap<ModelFromDB.RoomType, RoomTypeVM>();
            CreateMap<ModelFromDB.WishList, WishListVM>();

            //FRO //SAVING INTO THE DB
            CreateMap<HotelVM, ModelFromDB.Hotel>();
            CreateMap<PaymentVM, ModelFromDB.Payment>();
            CreateMap<RatingVM, ModelFromDB.Rating>();
            CreateMap<RoomVM, ModelFromDB.Room>();
            CreateMap<AddressVM, ModelFromDB.Address>();
            CreateMap<AmenityVM, ModelFromDB.Amenity>();
            CreateMap<AppUserVM, ModelFromDB.AppUser>();
            CreateMap<RoomTypeVM, ModelFromDB.RoomType>();
            CreateMap<WishListVM, ModelFromDB.WishList>();
            CreateMap<BaseEntityVM, ModelFromDB.BaseEntity>();
            CreateMap<BookingVM, ModelFromDB.Booking>();
            CreateMap<GalleryVM, ModelFromDB.Gallery>();
        }
    }
}
