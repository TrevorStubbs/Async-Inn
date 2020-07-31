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
    public class HotelRoomsTests : DatabaseTestBase
    {
        private IHotelRoom BuildRepoitory()
        {
            return new HotelRoomRepository(_db, _room);
        }

        [Fact]
        public async void CanCREATEANewHotelRoom()
        {

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = 1,
                RoomNumber = 300,
                Rate = 100.00m,
                PetFriendly = true,
                RoomId = 1
            };

            var repository = BuildRepoitory();

            var saved = await repository.Create(hotelRoom, 1);

            Assert.NotNull(saved);
            Assert.Equal(hotelRoom.RoomNumber, saved.RoomNumber);
        }

        [Fact]
        public async void CanGETAHotelRoom()
        {

            var repository = BuildRepoitory();

            var returnFromMethod = await repository.GetHotelRoom(1, 100);
            var expected = 100;

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnFromMethod.RoomNumber);
        }

        [Fact]
        public async void CanGETALLRooms()
        {

            var repository = BuildRepoitory();

            var returnFromMethod = await repository.GetHotelRooms(1);

            var returnList = new List<int>();

            foreach (var item in returnFromMethod)
            {
                returnList.Add(item.RoomNumber);
            }
            var expected = new List<int>
            {
                100, 200
            };

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnList);
        }

        [Fact]
        public async void CanUPDATEAHotelRoom()
        {
            var repository = BuildRepoitory();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = 1,
                RoomNumber = 100,
                Rate = 200.00m,
                PetFriendly = false,
                RoomId = 1
            };

            var expected = false;

            var returnFromMethod = await repository.Update(hotelRoom);

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnFromMethod.PetFriendly);
        }

        [Fact]
        public async void CanDeleteARoom()
        {
            var repository = BuildRepoitory();

            await repository.Delete(1, 100);
            var returnFromMethod = await repository.GetHotelRooms(1);

            var expected = new List<int>()
            {
                200
            };

            var returnList = new List<int>();

            foreach (var item in returnFromMethod)
            {
                returnList.Add(item.RoomNumber);
            }

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnList);
        }
    }
}
