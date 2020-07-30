using AsyncInn.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncInnTests
{
    public class DatabaseTestBase : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnDbContext _db;

        public DatabaseTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnDbContext(
                new DbContextOptionsBuilder<AsyncInnDbContext>()
                .UseSqlServer(_connection)
                .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
