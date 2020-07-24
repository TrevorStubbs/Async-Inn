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

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            /////////////////////// IF ERRORS LOOK HERE/////////////////
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            var room = await _context.Rooms.Where(x => x.Id == hotelRoom.RoomId)
                                            .Include(x => x.RoomAmenities)
                                            .ToListAsync();
                                            

            hotelRoom.Rooms = room;
            return hotelRoom;
                                         
         
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            /////////////////////// IF ERRORS LOOK HERE/////////////////
            var hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                      .Include(x => x.Room)
                                                      .ThenInclude(x => x.RoomAmenities)
                                                      .ToListAsync();

            return hotelRooms;
        }

        public async Task<HotelRoom> Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
