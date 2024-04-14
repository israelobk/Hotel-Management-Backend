using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Models;

namespace yard.infrastructure.Repositories.Interface
{
    public interface IRoomTypeRepository
    {
        Task<RoomType> GetRoomTypeById(int roomTypeId);
        Task UpdateRoomType(RoomType roomType);
        Task<bool> RoomTypeExistsAsync(int roomtypeId);
        Task<bool> RoomExistsAsync(int roomId);
        Task<Room> GetRoomByIdAsync(int roomId);
    }
}
