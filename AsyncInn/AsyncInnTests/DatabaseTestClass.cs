using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace AsyncInnTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddTransient<IAmenities, AmenitiesRepository>();
            service.AddTransient<IRoom, RoomRepository>();
            service.AddTransient<IHotelRoom, HotelRoomRepository>();
            service.AddTransient<IHotel, HotelRepository>();
        }
    }

    public class DatabaseTestBase : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnDbContext _db;
        protected readonly IAmenities _amenity;
        protected readonly IRoom _room;
        protected readonly IHotelRoom _hotelRoom;

        public DatabaseTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlite(_connection)
                .Options);

            _db.Database.EnsureCreated();

            _amenity = new AmenitiesRepository(_db);

            _room = new RoomRepository(_db, _amenity);

            _hotelRoom = new HotelRoomRepository(_db, _room);
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
