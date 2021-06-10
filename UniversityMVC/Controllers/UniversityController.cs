using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            University obj = new University();
            // Insert
            if (id == null)
            {
                return View(obj);
            }
            // Update
            obj = await _db.GetAsync(_url, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Here we only use GET and POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(University obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] pic = null;
                    using (var fs = files[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            pic = ms.ToArray();
                        }
                    }
                    obj.Picture = pic;
                }
                else if (obj.Id == 0)
                {
                    // If we're creating but not choose any Picture
                    obj.Picture = null;
                }
                else
                {
                    //Reuse old picture if we're updating and not choose any Picture
                    var objFromDb = await _db.GetAsync(_url, obj.Id);
                    obj.Picture = objFromDb.Picture;
                }

                //Create
                if (obj.Id == 0)
                {
                    await _db.CreateAsync(_url, obj);
                }
                //Update
                else
                {
                    await _db.UpdateAsync(_url, obj.Id, obj);
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
            return Json(new { data = await _db.GetAllAsync(_url) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _db.DeleteAsync(_url, id))
            {
                return Json(new { success = true, message = "Delete Successfully" });
            }
            return Json(new { success = false, message = "Delete Failure" });
        }
        #endregion
    }
}
