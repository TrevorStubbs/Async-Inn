using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Models.DTOs;

namespace AsyncInn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;
        private IRoom _rooms;

        /// <summary>
        /// Class constructor. Has the dependency injection.
        /// </summary>
        /// <param name="context">Takes a DbContext Object and a reference to the IRoom interface</param>
        public HotelRoomRepository(AsyncInnDbContext context, IRoom rooms)
        {
            _context = context;
            _rooms = rooms;
        }

        // Create
        /// <summary>
        /// Creates a new HotelRoomDTO
        /// </summary>
        /// <param name="hotelRoom">Takes in a HotelRoomDTO object</param>
        /// <returns>returns the created HotelRoomDTO</returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId)
        {

            HotelRoom hotelRoomDB = new HotelRoom()
            {
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomId,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly
            };

            _context.Entry(hotelRoomDB).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        // Read
        /// <summary>
        /// Gets a single hotel room based of its composite key
        /// </summary>
        /// <param name="hotelId">The id of the hotel its in</param>
        /// <param name="roomNumber">The room number of the hotel</param>
        /// <returns>Returns the selected hotelDTO</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                      .Include(x=>x.Hotel)
                                                      .Include(x => x.Room)                                                                    
                                                      .FirstOrDefaultAsync();

            RoomDTO dto = await _rooms.GetRoom(hotelRoom.RoomId);

            HotelRoomDTO hotelRoomDTO = new HotelRoomDTO()
            {
                HotelID = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomId = hotelRoom.RoomId,
                Room = dto
            };

            return hotelRoomDTO;
         
        }

        /// <summary>
        /// Gets all the Hotel Rooms of the selected hotel
        /// </summary>
        /// <param name="hotelId">the id of the hotel</param>
        /// <returns>all the hotelDTOs from the selected hotel</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                      .Include(x => x.Room)                                                      
                                                      .ToListAsync();

            List<HotelRoomDTO> dtoList = new List<HotelRoomDTO>();

            foreach(var room in hotelRooms)
            {
                var roomInfo = await _rooms.GetRoom(room.RoomId);

                dtoList.Add(new HotelRoomDTO()
                {
                    HotelID = room.HotelId,
                    RoomNumber = room.RoomNumber,
                    Rate = room.Rate,
                    PetFriendly = room.PetFriendly,
                    RoomId = room.RoomId,
                    Room = roomInfo
                });
            }

            return dtoList;
        }

        // Update
        /// <summary>
        /// Updates a hotelroom
        /// </summary>
        /// <param name="hotelRoom">Takes a hotelroomDTO object</param>
        /// <returns>the updated hotelDTO</returns>
        public async Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoom)
        {
            HotelRoom updatedHotelRoom = new HotelRoom() 
            {
                HotelId = hotelRoom.HotelID,
                RoomNumber = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomId,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly
            };


            _context.Entry(updatedHotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        // Delete
        /// <summary>
        /// Deletes the selected hotel
        /// </summary>
        /// <param name="hotelId">The hotel the room is in (int)</param>
        /// <param name="roomNumber">Its room number (int)</param>
        /// <returns>A completed task</returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            var deleteThis = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(deleteThis).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
