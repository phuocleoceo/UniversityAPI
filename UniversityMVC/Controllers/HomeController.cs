using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
            string token = HttpContext.Session.GetString("JWToken");
            HomeVM list = new HomeVM()
            {
                UniversityList = await _dbUNI.GetAllAsync(SD.UniversityAPIPath, token),
                PathWayList = await _dbPW.GetAllAsync(SD.PathWayAPIPath, token)
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
                TempData["Alert"] = "Login Failure ! Invalid Username or Password !";
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userLogin.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, userLogin.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", userLogin.Token);
            TempData["Alert"] = "Welcome " + userLogin.UserName;
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
                TempData["Alert"] = "Cannot Register ! This Username is Exists !";
                return View();
            }
            TempData["Alert"] = "Register Successfully !";
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            TempData["Alert"] = "Logout Successfully !";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
