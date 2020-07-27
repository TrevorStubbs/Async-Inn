using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenities
    {
        /// <summary>
        /// Creates a new Amenity
        /// </summary>
        /// <param name="amenity">Takes in an amenity object</param>
        /// <returns>A new amenity wrapped in a task</returns>
        // Create
        Task<AmenityDTO> Create(AmenityDTO amenity);

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">Takes an integer for id</param>
        /// <returns>returns the amenity object</returns>
        // Read
        Task<AmenityDTO> GetAmenity(int id);

        /// <summary>
        /// Gets all the amenities in the table
        /// </summary>
        /// <returns>returns all amenity objects</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Updates a single amenity
        /// </summary>
        /// <param name="amenity">takes a single amenity object</param>
        /// <returns>returns the updated object</returns>
        // Update
        Task<AmenityDTO> Update(AmenityDTO amenity, int id);

        // Delete
        /// <summary>
        /// Deletes a single amenity
        /// </summary>
        /// <param name="id">takes an integer for the id</param>
        /// <returns>A task complete</returns>
        Task Delete(int id);

    }
}
