using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityAPI.Models;

namespace UniversityAPI.Data.Configuration
{
    public class PathWayConfiguration : IEntityTypeConfiguration<PathWay>
    {
        public void Configure(EntityTypeBuilder<PathWay> builder)
        {
            builder.HasData
            (
                new PathWay
                {
                    Id = 1,
                    Name = "Nguyen Luong Bang",
                    Distance = 1,
                    Difficulty = PathWay.DifficultLevel.Medium,
                    UniversityId = 1,
                    DateCreated = DateTime.Now
                },
                new PathWay
                {
                    Id = 2,
                    Name = "Ngu Hanh Son",
                    Distance = 2,
                    Difficulty = PathWay.DifficultLevel.Easy,
                    UniversityId = 2,
                    DateCreated = DateTime.Now
                },
                new PathWay
                {
                    Id = 3,
                    Name = "Luong Nhu Hoc",
                    Distance = 1.5,
                    Difficulty = PathWay.DifficultLevel.Difficult,
                    UniversityId = 3,
                    DateCreated = DateTime.Now
                }
            );
        }
    }
}
