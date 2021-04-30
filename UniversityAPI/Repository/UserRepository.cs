using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data;
using UniversityAPI.Models;
using UniversityAPI.Repository.IRepository;

namespace UniversityAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly APIContext _db;
        public UserRepository(APIContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
