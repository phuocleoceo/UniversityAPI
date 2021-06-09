using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityMVC.Models;
using UniversityMVC.Repository.IRepository;

namespace UniversityMVC.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository _db;
        private readonly string _url;
        public UniversityController(IUniversityRepository db)
        {
            _db = db;
            _url = SD.UniversityAPIPath;
        }

        public IActionResult Index()
        {
            return View(new University { });
        }

        public async Task<IActionResult> GetAllUniversity()
        {
            return Json(new { data = await _db.GetAllAsync(_url) });
        }
    }
}
