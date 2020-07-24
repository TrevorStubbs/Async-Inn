using AsyncInn.Data;
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
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
            Amenity amenity = await GetAmenity(id);
            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all the amenities in the table
        /// </summary>
        /// <returns>returns all amenity objects</returns>
        public async Task<List<Amenity>> GetAmenities()
        {
            List<Amenity> amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">Takes an integer for id</param>
        /// <returns>returns the amenity object</returns>
        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);

            var roomAmenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                            .Include(x => x.Room)
                                                            .ToListAsync();
            amenity.RoomAmenities = roomAmenities;
            return amenity;
        }

        /// <summary>
        /// Updates a single amenity
        /// </summary>
        /// <param name="amenity">takes a single amenity object</param>
        /// <returns>returns the updated object</returns>
        // Update
        public async Task<Amenity> Update(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return amenity;
        }
    }
}
