using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Hotel
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }

        // Nav Properties
        public List<HotelRoom> HotelRooms { get; set; }
    }
}
