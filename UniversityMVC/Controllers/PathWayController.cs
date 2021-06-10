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

namespace UniversityMVC.Controllers
{
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

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<University> listUni = await _dbUNI.GetAllAsync(_urlUNI);

            PathWayVM pwVM = new PathWayVM()
            {
                UniversityList = listUni.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            // Insert
            if (id == null)
            {
                return View(pwVM);
            }
            // Update
            pwVM.PathWay = await _dbPW.GetAsync(_urlPW, id.GetValueOrDefault());
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
            if (ModelState.IsValid)
            {
                //Create
                if (obj.PathWay.Id == 0)
                {
                    await _dbPW.CreateAsync(_urlPW, obj.PathWay);
                }
                //Update
                else
                {
                    await _dbPW.UpdateAsync(_urlPW, obj.PathWay.Id, obj.PathWay);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }
        #region API Request
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _dbPW.GetAllAsync(_urlPW) });
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _dbPW.DeleteAsync(_urlPW, id))
            {
                return Json(new { success = true, message = "Delete Successfully" });
            }
            return Json(new { success = false, message = "Delete Failure" });
        }
        #endregion
    }
}
