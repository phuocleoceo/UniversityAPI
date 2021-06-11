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
                    UserName = "admin",
                    Password = "c4ca4238a0b923820dcc509a6f75849b",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    UserName = "guest",
                    Password = "c4ca4238a0b923820dcc509a6f75849b",
                    Role = "Guest"
                }
            );
        }
    }
}
