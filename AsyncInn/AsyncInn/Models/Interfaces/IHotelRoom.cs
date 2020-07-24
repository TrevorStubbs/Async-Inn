using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        // Create
        Task<HotelRoom> Create(HotelRoom hotelRoom);
        // Read
        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);
        Task<List<HotelRoom>> GetHotelRooms(int hotelId);
        // Update
        Task<HotelRoom> Update(HotelRoom hotelRoom);
        // Delete
        Task Delete(int hotelId, int roomNumber);

    }
}
