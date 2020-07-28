using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        // Create
        /// <summary>
        /// Creates a new HotelRoom
        /// </summary>
        /// <param name="hotelRoom">Takes in a HotelRoom object</param>
        /// <returns>returns the created HotelRoom</returns>
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId);

        // Read
        /// <summary>
        /// Gets a single hotel room based of its composite key
        /// </summary>
        /// <param name="hotelId">The id of the hotel its in</param>
        /// <param name="roomNumber">The room number of the hotel</param>
        /// <returns>Returns the selected hotel</returns>
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);

        /// <summary>
        /// Gets all the Hotel Rooms of the selected hotel
        /// </summary>
        /// <param name="hotelId">the id of the hotel</param>
        /// <returns>a list of all the hotels from the selected hotel</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        // Update
        /// <summary>
        /// Updates a hotelroom
        /// </summary>
        /// <param name="hotelRoom">Takes a hotelroom object</param>
        /// <returns>the updated hotel</returns>
        Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoom);

        // Delete
        /// <summary>
        /// Deletes the selected hotel
        /// </summary>
        /// <param name="hotelId">The hotel the room is in (int)</param>
        /// <param name="roomNumber">Its room number (int)</param>
        /// <returns>A completed task</returns>
        Task Delete(int hotelId, int roomNumber);

    }
}
