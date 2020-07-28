using AsyncInn.Data;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class AmenitiesRepository : IAmenities
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="context">Takes in a DbContext object</param>
        public AmenitiesRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Amenity
        /// </summary>
        /// <param name="amenity">Takes in an amenity object</param>
        /// <returns>A new amenitry wrapped in a task</returns>
        // Create
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            // Convert AmnityDTO --> Amenity

            Amenity entity = new Amenity()
            {
                Name = amenity.Name
            };

            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// Gets all the amenities in the table
        /// </summary>
        /// <returns>returns all amenity objects</returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            List<Amenity> amenities = await _context.Amenities.ToListAsync();

            List<AmenityDTO> dtos = new List<AmenityDTO>();

            foreach(var item in amenities)
            {
                dtos.Add(new AmenityDTO() { ID = item.Id, Name = item.Name });
            }

            return dtos;
        }

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">Takes an integer for id</param>
        /// <returns>returns the amenity object</returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            var roomAmenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                            .Include(x => x.Room)
                                                            .ThenInclude(x=>x.HotelRoom)
                                                            .ThenInclude(x=>x.Hotel)
                                                            .ToListAsync();
            amenity.RoomAmenities = roomAmenities;

            AmenityDTO dto = new AmenityDTO()
            {
                ID = amenity.Id,
                Name = amenity.Name
            };

            return dto;
        }

        /// <summary>
        /// Updates a single amenity
        /// </summary>
        /// <param name="amenity">takes a single amenity object</param>
        /// <returns>returns the updated object</returns>
        // Update
        public async Task<AmenityDTO> Update(AmenityDTO amenity, int id)
        {
            Amenity updatedAmenity = new Amenity()
            {
                Id = id,
                Name = amenity.Name
            };

            _context.Entry(updatedAmenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return amenity;
        }

        // Delete
        /// <summary>
        /// Deletes a single amenity
        /// </summary>
        /// <param name="id">takes an integer for the id</param>
        /// <returns>A task complete</returns>
        public async Task Delete(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
