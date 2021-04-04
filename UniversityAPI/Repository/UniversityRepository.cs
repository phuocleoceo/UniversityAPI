using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Data;
using UniversityAPI.Models;
using UniversityAPI.Repository.IRepository;

namespace UniversityAPI.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly APIContext _db;
        public UniversityRepository(APIContext db)
        {
            _db = db;
        }

        public bool CreateUniversity(University university)
        {
            _db.Universities.Add(university);
            return Save();
        }

        public bool DeleteUniversity(University university)
        {
            _db.Universities.Remove(university);
            return Save();
        }

        public ICollection<University> GetAll()
        {
            return _db.Universities.ToList();
        }

        public University GetById(int universityId)
        {
            return _db.Universities.FirstOrDefault(c => c.Id == universityId);
        }

        public bool UniversityExists(string Name)
        {
            return _db.Universities.Any(c => c.Name.ToLower().Trim() 
                                                == Name.ToLower().Trim());            
        }

        public bool UniversityExists(int id)
        {
            return _db.Universities.Any(c => c.Id == id);
        }

        public bool UpdateUniversity(University university)
        {
            _db.Universities.Update(university);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
