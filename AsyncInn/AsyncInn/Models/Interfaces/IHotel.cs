using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotel
    {
        // contain methods and properties that are required for the classes to implement

        // Create
        /// <summary>
        /// Creates a new Hotel
        /// </summary>
        /// <param name="hotel">Takes a hotel object</param>
        /// <returns>returns the created object</returns>
        Task<Hotel> Create(Hotel hotel);

        // Read
        // Get All
        /// <summary>
        /// Get's all the hotels in the table
        /// </summary>
        /// <returns>Returns all the hotel objects</returns>
        Task<List<HotelDTO>> GetHotels();

        // Get individually (by Id)
        /// <summary>
        /// Gets a single hotel from the table
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>the hotel object</returns>
        Task<HotelDTO> GetHotel(int id);

        // Update
        /// <summary>
        /// Updates a hotel
        /// </summary>
        /// <param name="hotel">takes in a hotel object to replace the old one</param>
        /// <returns>returns the updated hotel</returns>
        Task<Hotel> Update(Hotel hotel);

        // Delete
        /// <summary>
        /// Deletes a hotel
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>Completed Task</returns>
        Task Delete(int id);

    }
}
