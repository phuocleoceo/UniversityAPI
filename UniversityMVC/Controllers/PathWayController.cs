using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniversityMVC.Models;
using UniversityMVC.Models.ViewModel;
using UniversityMVC.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace UniversityMVC.Controllers
{
    [Authorize]
    public class PathWayController : Controller
    {
        private readonly IPathWayRepository _dbPW;
        private readonly IUniversityRepository _dbUNI;
        private readonly string _urlPW;
        private readonly string _urlUNI;
        public PathWayController(IPathWayRepository dbPW, IUniversityRepository dbUNI)
        {
            _dbPW = dbPW;
            _dbUNI = dbUNI;
            _urlPW = SD.PathWayAPIPath;
            _urlUNI = SD.UniversityAPIPath;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Upsert(int? id)
        {
            string token = HttpContext.Session.GetString("JWToken");
            IEnumerable<University> listUni = await _dbUNI.GetAllAsync(_urlUNI, token);

            PathWayVM pwVM = new PathWayVM()
            {
                UniversityList = listUni.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                PathWay = new PathWay()  //Has Id = 0 to check in Index View
            };
            // Insert
            if (id == null)
            {
                return View(pwVM);
            }
            // Update
            pwVM.PathWay = await _dbPW.GetAsync(_urlPW, id.GetValueOrDefault(), token);
            if (pwVM.PathWay == null)
            {
                return NotFound();
            }
            return View(pwVM);
        }

        //Here we only use GET and POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PathWayVM obj)
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (ModelState.IsValid)
            {
                //Create
                if (obj.PathWay.Id == 0)
                {
                    await _dbPW.CreateAsync(_urlPW, obj.PathWay, token);
                }
                //Update
                else
                {
                    await _dbPW.UpdateAsync(_urlPW, obj.PathWay.Id, obj.PathWay, token);
                }
                TempData["Alert"] = "Modify Successfully !";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Fix the validation not working when Create with 0 information added
                IEnumerable<University> listUni = await _dbUNI.GetAllAsync(_urlUNI, token);
                PathWayVM pwVM = new PathWayVM()
                {
                    UniversityList = listUni.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    PathWay = obj.PathWay
                };
                return View(pwVM);
            }
        }
        #region API Request
        public async Task<IActionResult> GetAll()
        {
            string token = HttpContext.Session.GetString("JWToken");
            return Json(new { data = await _dbPW.GetAllAsync(_urlPW, token) });
        }

        [HttpDelete]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (await _dbPW.DeleteAsync(_urlPW, id, token))
            {
                return Json(new { success = true, message = "Delete Successfully" });
            }
            return Json(new { success = false, message = "Delete Failure" });
        }
        #endregion
    }
}
