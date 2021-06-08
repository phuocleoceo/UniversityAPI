using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UniversityMVC.Models;
using UniversityMVC.Repository.IRepository;

namespace UniversityMVC.Repository
{
    public class PathWayRepository : Repository<PathWay>, IPathWayRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public PathWayRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
