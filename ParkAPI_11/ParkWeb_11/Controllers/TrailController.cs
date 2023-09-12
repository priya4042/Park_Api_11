﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkWeb_11.Models;
using ParkWeb_11.Models.ViewModels;
using ParkWeb_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;
        public TrailController(ITrailRepository trailRepository,INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _trailRepository.GetAllAsync(SD.TrailAPIPath) });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVM trailVM = new TrailVM()

            {
                Trail = new Trail(),
                nationalParkList = nationalParks.Select(np => new SelectListItem()
                {
                    Text = np.Name,
                    Value = np.Id.ToString()

                })
                
            };
            if (id == null)
                return View(trailVM);
            else
                trailVM.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());
            if (trailVM.Trail == null)
                return NotFound();
            return View(trailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if(ModelState.IsValid)
            {
                if (trailVM.Trail.Id == 0)
                    await _trailRepository.CreateAsync(SD.TrailAPIPath, trailVM.Trail);
                else
                    await _trailRepository.UpdateAsync(SD.TrailAPIPath, trailVM.Trail);
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<NationalPark> nationalParks = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
                trailVM = new TrailVM()
                {
                    Trail = new Trail(),
                    nationalParkList = nationalParks.Select(np => new SelectListItem()
                    {
                        Text=np.Name,
                        Value=np.Id.ToString()

                    })
                  };
                return View(trailVM);
            }
        }

        #region API's
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if (status)
                return Json(new { success = true, message = "Data Deleted Successfully!!" });
            else
                return Json(new { success = false, message = "Error while Deleting Data!!!" });
        }
        #endregion
    }

}
