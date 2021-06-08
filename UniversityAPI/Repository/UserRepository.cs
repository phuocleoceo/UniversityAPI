using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UniversityAPI.Data;
using UniversityAPI.Models;
using UniversityAPI.Repository.IRepository;
using UniversityAPI.Utility;

namespace UniversityAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly APIContext _db;
        private readonly AppSettings _appSettings;
        public UserRepository(APIContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public bool IsUniqueUser(string username)
        {
            //Single() must the only one, First() can be many obj
            var user = _db.Users.SingleOrDefault(c => c.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public User Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(c => c.UserName == username && c.Password == password);
            //User not found
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";  //Not Save() so it's not affect to database
            return user;
        }

        public User Register(string username, string password)
        {
            User user = new User()
            {
                UserName = username,
                Password = password,
                Role = SD.Role_Admin
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            user.Password = "";
            return user;
        }
    }
}
