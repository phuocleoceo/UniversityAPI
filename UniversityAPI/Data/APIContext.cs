using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data.Configuration;
using UniversityAPI.Models;

namespace UniversityAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UniversityConfiguration());
            modelBuilder.ApplyConfiguration(new PathWayConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<University> Universities { get; set; }
        public DbSet<PathWay> PathWays { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
