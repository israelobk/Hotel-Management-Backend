using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Models;
using yard.domain.ViewModels;



namespace yard.application.Services.Interface
{
    public interface IBookingService
    {
        Task<BookingVM> AddBookingAsync(BookingVM booking);

        Task<bool> RoomExistAsync(int roomId);

        Task<Room> GetRoomByIdAsync(int roomId);

        Task<bool> RoomTypeExistAsync(int roomTypeId);
    }
}
