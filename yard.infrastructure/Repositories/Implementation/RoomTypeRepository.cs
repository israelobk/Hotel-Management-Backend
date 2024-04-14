using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;

namespace yard.infrastructure.Repositories.Implementation
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public RoomTypeRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomType> GetRoomTypeById(int roomTypeId)
        {
            return await _context.RoomTypes.Include(r => r.Rooms).FirstOrDefaultAsync(r => r.Id == roomTypeId); //check if hotel exist first
        }

        public async Task<bool> RoomTypeExistsAsync(int roomTypeId)
        {
            return await _context.RoomTypes.AnyAsync(r => r.Id == roomTypeId);
        }

        public async Task<Room> GetRoomByIdAsync(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            var roomToReturn = _mapper.Map<RoomVM>(room);
            return room;
        }

        public async Task<bool> RoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public async Task UpdateRoomType(RoomType roomType)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync();
        }
    }
}
