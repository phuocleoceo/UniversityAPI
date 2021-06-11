using Microsoft.AspNetCore.Http;
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
        private readonly IUserRepository _dbUSER;

        public HomeController(ILogger<HomeController> logger,
                              IUniversityRepository dbUNI,
                              IPathWayRepository dbPW,
                              IUserRepository dbUSER)
        {
            _logger = logger;
            _dbUNI = dbUNI;
            _dbPW = dbPW;
            _dbUSER = dbUSER;
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

        [HttpGet]
        public IActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            User userLogin = await _dbUSER.LoginAsync(SD.UserAPIPath + "authenticate/", user);
            if (userLogin.Token == null)
            {
                return View();
            }
            HttpContext.Session.SetString("JWToken", userLogin.Token);
            HttpContext.Session.SetString("CurrentUserName", userLogin.UserName);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            bool result = await _dbUSER.RegisterAsync(SD.UserAPIPath + "register/", user);
            if (!result)
            {
                return View();
            }
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("JWToken", "");
            HttpContext.Session.SetString("CurrentUserName", "");
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
