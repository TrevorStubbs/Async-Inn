using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);

        Task<List<Room>> GetRooms();
        Task<Room> GetRoom(int id);

        Task<Room> Update(Room room);

        Task Delete(int id);
    }
}
