using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomRepository : IRoom
    {
        // inject the DB Context
        private AsyncInnDbContext _context;

        /// <summary>
        /// Class constructor. Has the dependency injection.
        /// </summary>
        /// <param name="context">Takes a DbContext Object</param>
        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
            // inject IAmenity amnities
            // _amenities= amenities
        }

        /// <summary>
        /// Creates a new hotel
        /// </summary>
        /// <param name="room">Take a hotel object</param>
        /// <returns>Returns the created hotel</returns>
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();

            return room;
        }

        /// <summary>
        /// Deletes the selected object
        /// </summary>
        /// <param name="id">The int of the room object</param>
        /// <returns>A completed task</returns>
        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a room object by id
        /// </summary>
        /// <param name="id">takes and int for the id</param>
        /// <returns>A single room object</returns>
        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            var amenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                        .Include(x => x.Amenity)
                                                        .ToListAsync();
            room.RoomAmenities = amenities;

            // get list of aminty DTOs
            // _amenities.getameinity(id)
            // foeach (var item in amenites
            // getamenity(id)
            return room;
        }

        /// <summary>
        /// Gets all the rooms in the table
        /// </summary>
        /// <returns>A list of all the rooms in the table</returns>
        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.Include(x=>x.RoomAmenities)
                                            .ThenInclude(x=>x.Amenity)
                                            .ToListAsync();
           
            return rooms;
        }

        /// <summary>
        /// Updates a single room
        /// </summary>
        /// <param name="room">Takes a room object</param>
        /// <returns>Returns the updated room object</returns>
        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return room;
        }

        /// <summary>
        /// Adds an amenity to the room
        /// </summary>
        /// <param name="amenityId">int id of the amenity</param>
        /// <param name="roomId">int id of the room</param>
        /// <returns>a completed task</returns>
        public async Task AddRoomAmenity(int amenityId, int roomId)
        {
            RoomAmenities roomAmenity = new RoomAmenities()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the selected amenity from the selected room
        /// </summary>
        /// <param name="amenityId">int id of the amenity</param>
        /// <param name="roomId">int id of the room</param>
        /// <returns>a completed task</returns>
        public async Task RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(x => x.AmenityId == amenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
