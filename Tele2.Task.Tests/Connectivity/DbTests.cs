using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Tele2.Task.Interaction;
using System.Linq;
using System;
using Tele2.Task.Models;

namespace Tele2.Task.Tests.Connectivity
{
    [TestFixture]
    public class DbTests
    {
        DwellersContext client;

        [SetUp]
        public void Configure()
        {
            string dbName = $"test_db_{Guid.NewGuid().ToString().Split('-').First()}";
            string connectionString = $"server=localhost;user=root;password=12345678;database={dbName};";
            string mySqlVersion = "8.0.27";

            var builder = new DbContextOptionsBuilder<DwellersContext>();
            builder.UseMySql(connectionString, new MySqlServerVersion(mySqlVersion));
            client = new(builder.Options);
            client.Database.EnsureCreated();
        }

        [Test]
        public void ConnectsToDb()
        {
            Assert.IsTrue(client.Database.CanConnect());
        }

        [Test]
        public void PushesToDatabase()
        {
            var dwellers = new Dweller[]
            {
                new() { Name = "Aaaa Bbbb", Age = 1, Id = "aaaaaaaa", Sex = "male"},
                new() { Name = "Cccc Dddd", Age = 2, Id = "aaaaaaab", Sex = "female"},
                new() { Name = "Eeee Ffff", Age = 3, Id = "aaaaaaac", Sex = "male"}
            };
            client.Dwellers.AddRange(dwellers);
            Assert.AreEqual(dwellers.Length, client.SaveChanges());
        }

        [Test]
        public void PullsFromDatabase()
        {
            var dweller = new Dweller() { Name = "Alice Bobova", Age = 18, Id = "dddddddd", Sex = "female" };
            client.Dwellers.Add(dweller);
            client.SaveChanges();
            Assert.IsTrue(client.Dwellers.Contains(dweller));
        }

        [TearDown]
        public void Deconfigure()
        {
            client.Database.EnsureDeleted();
            client.Dispose();
        }
    }
}
