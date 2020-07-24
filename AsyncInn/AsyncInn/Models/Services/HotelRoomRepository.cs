using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Class constructor. Has the dependency injection.
        /// </summary>
        /// <param name="context">Takes a DbContext Object</param>
        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        // Create
        /// <summary>
        /// Creates a new HotelRoom
        /// </summary>
        /// <param name="hotelRoom">Takes in a HotelRoom object</param>
        /// <returns>returns the created HotelRoom</returns>
        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        // Read
        /// <summary>
        /// Gets a single hotel room based of its composite key
        /// </summary>
        /// <param name="hotelId">The id of the hotel its in</param>
        /// <param name="roomNumber">The room number of the hotel</param>
        /// <returns>Returns the selected hotel</returns>
        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            var room = await _context.Rooms.Where(x => x.Id == hotelRoom.RoomId)
                                            .Include(x => x.RoomAmenities)
                                            .ThenInclude(x=>x.Amenity)
                                            .ToListAsync();
                                            
            return hotelRoom;
                                         
         
        }

        /// <summary>
        /// Gets all the Hotel Rooms of the selected hotel
        /// </summary>
        /// <param name="hotelId">the id of the hotel</param>
        /// <returns>all the hotels from the selected hotel</returns>
        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                      .Include(x => x.Room)
                                                      .ThenInclude(x => x.RoomAmenities)
                                                      .ThenInclude(x => x.Amenity)
                                                      .ToListAsync();

            return hotelRooms;
        }

        // Update
        /// <summary>
        /// Updates a hotelroom
        /// </summary>
        /// <param name="hotelRoom">Takes a hotelroom object</param>
        /// <returns>the updated hotel</returns>
        public async Task<HotelRoom> Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
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
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
