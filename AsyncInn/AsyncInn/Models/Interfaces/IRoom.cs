using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a new hotel
        /// </summary>
        /// <param name="room">Take a hotel object</param>
        /// <returns>Returns the created hotel</returns>
        Task<Room> Create(Room room);

        /// <summary>
        /// Gets all the rooms in the table
        /// </summary>
        /// <returns>A list of all the rooms in the table</returns>
        Task<List<Room>> GetRooms();

        /// <summary>
        /// Gets a room object by id
        /// </summary>
        /// <param name="id">takes and int for the id</param>
        /// <returns>A single room object</returns>
        Task<RoomDTO> GetRoom(int id);

        /// <summary>
        /// Updates a single room
        /// </summary>
        /// <param name="room">Takes a room object</param>
        /// <returns>Returns the updated room object</returns>
        Task<Room> Update(Room room);

        /// <summary>
        /// Deletes the selected object
        /// </summary>
        /// <param name="id">The int of the room object</param>
        /// <returns>A completed task</returns>
        Task Delete(int id);

        /// <summary>
        /// Adds an amenity to the room
        /// </summary>
        /// <param name="amenityId">int id of the amenity</param>
        /// <param name="roomId">int id of the room</param>
        /// <returns>a completed task</returns>
        Task AddRoomAmenity(int amenityId, int roomId);

        /// <summary>
        /// Deletes the selected amenity from the selected room
        /// </summary>
        /// <param name="amenityId">int id of the amenity</param>
        /// <param name="roomId">int id of the room</param>
        /// <returns>a completed task</returns>
        Task RemoveAmenityFromRoom(int amenityId, int roomId);
    }
}
