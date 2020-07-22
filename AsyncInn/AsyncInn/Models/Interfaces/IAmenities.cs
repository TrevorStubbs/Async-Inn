using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenities
    {
        // Create
        Task<Amenity> Create(Amenity amenity);

        // Read
        Task<Amenity> GetAmenity(int id);
        Task<List<Amenity>> GetAmenities();

        // Update
        Task<Amenity> Update(Amenity amenity);

        // Delete
        Task Delete(int id);
    }
}
