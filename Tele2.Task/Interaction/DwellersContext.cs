using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tele2.Task.Models;

namespace Tele2.Task.Interaction
{
    public class DwellersContext : DbContext
    {
        public DbSet<Dweller> Dwellers { get; set; }

        public DwellersContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
