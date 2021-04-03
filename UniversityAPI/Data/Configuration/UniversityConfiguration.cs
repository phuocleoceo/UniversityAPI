using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityAPI.Models;

namespace UniversityAPI.Data.Configuration
{
    public class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasData
            (
                new University { Id = 1, Name = "Bách khoa Đà Nẵng", Abbreviation = "DUT", Address = "Đà Nẵng", Established = new DateTime(1975, 7, 15) },
                new University { Id = 2, Name = "Kinh tế Đà Nẵng", Abbreviation = "DUE", Address = "Đà Nẵng", Established = new DateTime(1975, 11, 20) }
            );
        }
    }
}
