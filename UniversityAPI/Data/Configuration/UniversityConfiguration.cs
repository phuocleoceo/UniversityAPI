using System;
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
                new University { Id = 2, Name = "Kinh tế Đà Nẵng", Abbreviation = "DUE", Address = "Đà Nẵng", Established = new DateTime(1975, 11, 20) },
                new University { Id = 3, Name = "Ngoại Ngữ Đà Nẵng", Abbreviation = "UFL", Address = "Đà Nẵng", Established = new DateTime(2002, 08, 26) },
                new University { Id = 4, Name = "Sư phạm kĩ thuật Đà Nẵng", Abbreviation = "UTE", Address = "Đà Nẵng", Established = new DateTime(2017, 01, 01) },
                new University { Id = 5, Name = "Sư phạm Đà Nẵng", Abbreviation = "UED", Address = "Đà Nẵng", Established = new DateTime(1994, 04, 04) },
                new University { Id = 6, Name = "Khoa Y Dược Đà Nẵng", Abbreviation = "SMP", Address = "Đà Nẵng", Established = new DateTime(2007, 01, 01) },
                new University { Id = 7, Name = "CNTT Việt-Hàn Đà Nẵng", Abbreviation = "VKU", Address = "Đà Nẵng", Established = new DateTime(2020, 01, 03) }
            );
        }
    }
}
