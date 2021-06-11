using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string url, string token);

        Task<T> GetAsync(string url, int Id, string token);

        Task<bool> CreateAsync(string url, T obj, string token);

        Task<bool> UpdateAsync(string url, int id, T obj, string token);

        Task<bool> DeleteAsync(string url, int Id, string token);
    }
}
