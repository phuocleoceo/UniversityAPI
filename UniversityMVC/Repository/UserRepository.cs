using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversityMVC.Models;
using UniversityMVC.Repository.IRepository;

namespace UniversityMVC.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> LoginAsync(string url, User obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(obj),
                                                    Encoding.UTF8,
                                                    "application/json");
            }
            else
            {
                return new User();
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(jsonString);
            }
            else
            {
                return new User();
            }
        }

        public async Task<bool> RegisterAsync(string url, User obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(obj),
                                                    Encoding.UTF8,
                                                    "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
