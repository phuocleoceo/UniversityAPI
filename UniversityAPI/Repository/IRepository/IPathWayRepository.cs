using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Repository.IRepository
{
    public interface IPathWayRepository
    {
        ICollection<PathWay> GetAll();

        PathWay GetById(int pathWayId);

        bool PathWayExists(string Name);

        bool PathWayExists(int id);

        bool CreatePathWay(PathWay pathWay);

        bool UpdatePathWay(PathWay pathWay);

        bool DeletePathWay(PathWay pathWay);

        bool Save();
    }
}
