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
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();

            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            var amenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                        .Include(x => x.Amenity)
                                                        .ToListAsync();
            room.RoomAmenities = amenities;
            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.Include(x=>x.RoomAmenities)
                                            .ThenInclude(x=>x.Amenity)
                                            .ToListAsync();
           
            return rooms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return room;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amenityId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="amenityId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public async Task RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(x => x.AmenityId == amenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
