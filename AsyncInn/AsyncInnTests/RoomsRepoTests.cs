using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AsyncInnTests
{
    public class RoomsRepoTests : DatabaseTestBase
    {
        private IRoom BuildRepoitory()
        {
            return new RoomRepository(_db, _amenity);
        }

        [Fact]
        public async void CanCREATEANewRoom()
        {
            
            var room = new RoomDTO()
            {
                Name = "Lava Lounge",
                Layout = Layout.Studio.ToString()
            };

            var repository = BuildRepoitory();

            var saved = await repository.Create(room);

            Assert.NotNull(saved);
            Assert.Equal(room.Name, saved.Name);
        }

        [Fact]
        public async void CanGETARoom()
        {
 
            var repository = BuildRepoitory();

            var returnFromMethod = await repository.GetRoom(1);
            var expected = "The Workshop";

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnFromMethod.Name);
        }

        //[Fact]
        //public async void CanGETALLRooms()
        //{

        //    var repository = BuildRepoitory();

        //    var returnFromMethod = await repository.GetRooms();
        //    var expected = new List<string>
        //    {
        //        "The Workshop", "London Flat", "Icheon Penthouse"
        //    };

        //    Assert.NotNull(returnFromMethod);
        //    Assert.Equal(expected, returnFromMethod);
        //}
    }
}
