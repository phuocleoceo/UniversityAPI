using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityMVC.Models;

namespace UniversityMVC.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string url, User obj);

        Task<bool> RegisterAsync(string url, User obj);
    }
}
