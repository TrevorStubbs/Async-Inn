using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout Layout { get; set; }

        public List<RoomAmenities> RoomAmenities { get; set; }
        public List<HotelRoom> HotelRoom { get; set; }
    }

    public enum Layout
    {
        Studio=0,
        OneBedroom,
        TwoBedroom
    }
}
