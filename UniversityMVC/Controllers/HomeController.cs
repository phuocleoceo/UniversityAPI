using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UniversityMVC.Models;
using UniversityMVC.Models.ViewModel;
using UniversityMVC.Repository.IRepository;

namespace UniversityMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUniversityRepository _dbUNI;
        private readonly IPathWayRepository _dbPW;

        public HomeController(ILogger<HomeController> logger,
                              IUniversityRepository dbUNI,
                              IPathWayRepository dbPW)
        {
            _logger = logger;
            _dbUNI = dbUNI;
            _dbPW = dbPW;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM list = new HomeVM()
            {
                UniversityList = await _dbUNI.GetAllAsync(SD.UniversityAPIPath),
                PathWayList = await _dbPW.GetAllAsync(SD.PathWayAPIPath)
            };
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
