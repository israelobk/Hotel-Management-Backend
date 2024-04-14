using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Models;

namespace yard.application.Services.Interface
{
    public interface IRoomService
    {
        Task<int> GetAvailableRooms(int hotelId, int roomTypeId);
        RoomTypeResult GetRoom(int hotelId, int roomtypeId);
    }
}
