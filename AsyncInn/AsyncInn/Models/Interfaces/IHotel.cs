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
        /// <param name="hotel">Takes a hotelDTO object</param>
        /// <returns>returns the created objectDTO</returns>
        Task<HotelDTO> Create(HotelDTO hotel);

        // Read
        // Get All
        /// <summary>
        /// Get's all the hotels in the table
        /// </summary>
        /// <returns>Returns all the hotelDTO objects</returns>
        Task<List<HotelDTO>> GetHotels();

        // Get individually (by Id)
        /// <summary>
        /// Gets a single hotelDTO from the table
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>the hotelDTO object</returns>
        Task<HotelDTO> GetHotel(int id);

        // Update
        /// <summary>
        /// Updates a hotel
        /// </summary>
        /// <param name="hotel">takes in a hotelDTO object to replace the old one</param>
        /// <returns>returns the updated hotelDTO</returns>
        Task<HotelDTO> Update(HotelDTO hotel);

        // Delete
        /// <summary>
        /// Deletes a hotel
        /// </summary>
        /// <param name="id">Integer for the id</param>
        /// <returns>Completed Task</returns>
        Task Delete(int id);

    }
}
