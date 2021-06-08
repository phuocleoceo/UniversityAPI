using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversityMVC.Repository.IRepository;

namespace UniversityMVC.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<IEnumerable<T>> GetAllAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(string url, int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj != null)
            {
                request.Content = new StringContent(
                        JsonConvert.SerializeObject(obj),
                        Encoding.UTF8,
                        "application/json");
            }
            else
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> UpdateAsync(string url, T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string url, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
