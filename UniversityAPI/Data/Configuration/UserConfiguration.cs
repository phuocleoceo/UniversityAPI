using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityAPI.Models;

namespace UniversityAPI.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = 1,
                    UserName = "phuoc",
                    Password = "1",
                    Role = "Admin"
                }
            );
        }
    }
}
