using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.application.Services.Interface;
using yard.domain.Models;
using yard.infrastructure.Repositories.Interface;

namespace yard.application.Services.Implementation
{
    public class RoomService : IRoomService
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IHotelService hotelService, IRoomTypeRepository roomTypeRepository, ILogger<RoomService> logger)
        {
            _hotelService = hotelService;
            _roomTypeRepository = roomTypeRepository;
            _logger = logger;
        }

        public async Task<int> GetAvailableRooms(int hotelId, int roomTypeId)
        {
            try
            {
                var hotelExists = await _hotelService.HotelExistAsync(hotelId);
                if (hotelExists)
                {
                    bool roomExists = await _roomTypeRepository.RoomTypeExistsAsync(roomTypeId);
                    if (roomExists)
                    {
                        RoomType roomType = await _roomTypeRepository.GetRoomTypeById(roomTypeId);
                        if (roomType != null)
                        {
                            int availableRooms = roomType.AvailableRooms;
                            return availableRooms;
                        }
                        else
                        {
                            throw new InvalidOperationException($"Room type with Id {roomTypeId} not found.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Room with room type Id {roomTypeId} not found");
                    }
                    
                }
                else
                {
                    throw new InvalidOperationException($"Hotel with Id {hotelId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Available rooms");
                throw;
            }
        }

        public RoomTypeResult GetRoom(int hotelId, int roomtypeId)
        {
            try
            {
                var hotelExists = _hotelService.HotelExistAsync(hotelId).Result;
                if (hotelExists)
                {
                    var roomTypeExists = _roomTypeRepository.RoomTypeExistsAsync(roomtypeId).Result;
                    if (roomTypeExists)
                    {
                        RoomType roomtype = _roomTypeRepository.GetRoomTypeById(roomtypeId).Result;
                        if (roomtype != null)
                        {
                            return new RoomTypeResult { RoomType = roomtype };
                        }
                        else
                        {
                            return new RoomTypeResult { ErrorMessage = $"Roomtype object does not exist" };
                        }
                    }
                    else
                    {
                        return new RoomTypeResult { ErrorMessage = $"Room type with Id {roomtypeId} not found." };
                    }
                }
                else
                {
                    return new RoomTypeResult { ErrorMessage = $"Hotel with Id {hotelId} not found." };
                }
            }
            catch (Exception ex)
            {
                return new RoomTypeResult { ErrorMessage = ex.Message };
            }
        }
    }
}
