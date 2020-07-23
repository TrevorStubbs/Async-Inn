using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>
        // Create
        Task<Amenity> Create(Amenity amenity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Read
        Task<Amenity> GetAmenity(int id);
        Task<List<Amenity>> GetAmenities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amenity"></param>
        /// <returns></returns>
        // Update
        Task<Amenity> Update(Amenity amenity);

        // Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amenityId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task AddRoom(int amenityId, int roomId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amenityId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task RemoveRoomFromAmenity(int amenityId, int roomId);
    }
}
