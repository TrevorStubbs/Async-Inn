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
        /// Creates a new RoomDTO
        /// </summary>
        /// <param name="room">Take a RoomDTO object</param>
        /// <returns>Returns the created RoomDTO</returns>
        Task<RoomDTO> Create(RoomDTO room);

        /// <summary>
        /// Gets all the roomDTOs in the table
        /// </summary>
        /// <returns>A list of all the roomDTOs in the table</returns>
        Task<List<RoomDTO>> GetRooms();

        /// <summary>
        /// Gets a roomDTO object by id
        /// </summary>
        /// <param name="id">takes and int for the id</param>
        /// <returns>A single roomDTO object</returns>
        Task<RoomDTO> GetRoom(int id);

        /// <summary>
        /// Updates a single roomDTO
        /// </summary>
        /// <param name="room">Takes a roomDTO object</param>
        /// <returns>Returns the updated roomDTO object</returns>
        Task<RoomDTO> Update(RoomDTO room, int id);

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
