using AsyncInn.Data;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;
        private IHotelRoom _hotelRoom;

        /// <summary>
        /// This is the constructor that has the dependency injection in it.
        /// </summary>
        /// <param name="context">Takes a DbContext Object</param>
        public HotelRepository(AsyncInnDbContext context, IHotelRoom hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }

        /// <summary>
        /// Creates a new Hotel
        /// </summary>
        /// <param name="hotel">Takes a hotel object</param>
        /// <returns>returns the created object</returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            await _context.SaveChangesAsync();

            return hotel;
        }

        // Get individually (by Id)
        /// <summary>
        /// Gets a single hotel from the table
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>the hotel object</returns>
        public async Task<HotelDTO> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels.Where(x=>x.Id == id)
                                              .Include(x => x.HotelRooms)
                                              .ThenInclude(x => x.Room)
                                              .ThenInclude(x => x.RoomAmenities)
                                              .ThenInclude(x => x.Amenity)
                                              .FirstOrDefaultAsync();

            List<HotelRoomDTO> hotelRooms = await _hotelRoom.GetHotelRooms(id);

            HotelDTO hotelDTO = new HotelDTO()
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotelRooms
            };

            return hotelDTO;
        }

        // Read
        // Get All
        /// <summary>
        /// Get's all the hotels in the table
        /// </summary>
        /// <returns>Returns all the hotel objects</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            List<Hotel> allHotels = await _context.Hotels.ToListAsync();

            List<HotelDTO> hotelDTOs = new List<HotelDTO>();

            foreach (var hotel in allHotels)
            {
                hotelDTOs.Add(new HotelDTO()
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone
                });
            }

            return hotelDTOs;
        }

        // Update
        /// <summary>
        /// Updates a hotel
        /// </summary>
        /// <param name="hotel">takes in a hotel object to replace the old one</param>
        /// <returns>returns the updated hotel</returns>
        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotel;
        }

        // Delete
        /// <summary>
        /// Deletes a hotel
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>Completed Task</returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync();
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
