using AsyncInn.Models;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using System;
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
        public async void CanSaveANewAmenity()
        {
            // Arrange
            var amenity = new AmenityDTO
            {
                ID = 1,
                Name = "Pool"
            };

            var repository = BuildRepoitory();

            var saved = await repository.Create(amenity);

            Assert.NotNull(saved);


        }
    }
}
