using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Repository.IRepository
{
    public interface IUniversityRepository
    {
        ICollection<University> GetAll();

        University GetById(int universityId);

        bool UniversityExists(string Name);

        bool UniversityExists(int id);

        bool CreateUniversity(University university);

        bool UpdateUniversity(University university);

        bool DeleteUniversity(University university);

        bool Save();
    }
}
