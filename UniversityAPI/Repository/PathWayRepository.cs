using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data;
using UniversityAPI.Models;
using UniversityAPI.Repository.IRepository;

namespace UniversityAPI.Repository
{
    public class PathWayRepository : IPathWayRepository
    {
        private readonly APIContext _db;
        public PathWayRepository(APIContext db)
        {
            _db = db;
        }

        public bool CreatePathWay(PathWay pathWay)
        {
            _db.PathWays.Add(pathWay);
            return Save();
        }

        public bool DeletePathWay(PathWay pathWay)
        {
            _db.PathWays.Remove(pathWay);
            return Save();
        }

        public ICollection<PathWay> GetAll()
        {
            return _db.PathWays.ToList();
        }

        public PathWay GetById(int pathWayId)
        {
            return _db.PathWays.FirstOrDefault(c => c.Id == pathWayId);
        }

        public bool PathWayExists(string Name)
        {
            return _db.PathWays.Any(c => c.Name.ToLower().Trim() 
                                                == Name.ToLower().Trim());            
        }

        public bool PathWayExists(int id)
        {
            return _db.PathWays.Any(c => c.Id == id);
        }

        public bool UpdatePathWay(PathWay pathWay)
        {
            _db.PathWays.Update(pathWay);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
