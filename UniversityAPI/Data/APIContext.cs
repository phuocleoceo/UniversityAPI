using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data.Configuration;
using UniversityAPI.Models;

namespace UniversityAPI.Data
{
    public class APIContext:DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UniversityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<University> Universities { get; set; }
    }
}
