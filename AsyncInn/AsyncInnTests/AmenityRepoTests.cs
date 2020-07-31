using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace AsyncInnTests
{
    public class AmenityRepoTests : DatabaseTestBase
    {
        private IAmenities BuildRepoitory()
        {
            return new AmenitiesRepository(_db);
        }

        [Fact]
        public async void CanCREATEANewAmenity()
        {
            // Arrange
            var amenity = new AmenityDTO
            {
                Name = "Pool"
            };

            var repository = BuildRepoitory();

            var saved = await repository.Create(amenity);

            Assert.NotNull(saved);
            Assert.Equal(amenity.Name, saved.Name);
        }

        [Fact]
        public async void CanREADAmenity()
        {
            // Arrange

            var repository = BuildRepoitory();

            var expected = "Infinity Pool";
            var returnFromMethod = await repository.GetAmenity(3);

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnFromMethod.Name);
        }

        [Fact]
        public async void CanREADALLAmenities()
        {
            // Arrange

            var repository = BuildRepoitory();

            var expected = new List<string>()
            {
                "Workbench", "Tea Pot", "Infinity Pool"
            };

            var returnFromMethod = await repository.GetAmenities();

            var returnList = new List<string>();

            foreach (var item in returnFromMethod)
            {
                returnList.Add(item.Name);
            }

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnList);
        }

        [Fact]
        public async void CanUPDATEAnAmenity()
        {
            // Arrange
            var amenity = new AmenityDTO
            {
                ID = 1,
                Name = "Lava Rocks"
            };

            var repository = BuildRepoitory();

            await repository.Update(amenity, 1);
            var returnFromMethod = await repository.GetAmenity(1);

            Assert.NotNull(returnFromMethod);
            Assert.Equal(amenity.Name, returnFromMethod.Name);
        }

        [Fact]
        public async void CanDELETEAnAmenity()
        {

            var repository = BuildRepoitory();

            await repository.Delete(1);
            var returnFromMethod = await repository.GetAmenities();

            var expected = new List<string>() 
            {
                "Tea Pot", "Infinity Pool"
            };

            var returnList = new List<string>();

            foreach (var item in returnFromMethod)
            {
                returnList.Add(item.Name);
            }

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnList);
        }
    }
}
