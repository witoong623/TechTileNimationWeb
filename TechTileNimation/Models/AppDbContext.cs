using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTileNimation.Models;

namespace TechTileNimation.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensationEntry> SensationEntry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensationEntry>();
        }
    }
}
