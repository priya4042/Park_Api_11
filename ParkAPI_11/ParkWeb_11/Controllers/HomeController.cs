﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkWeb_11.Models;
using ParkWeb_11.Models.ViewModels;
using ParkWeb_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb_11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailRepository _trailRepository;

        public HomeController(ILogger<HomeController> logger,INationalParkRepository nationalParkRepository,ITrailRepository trailRepository)
        {
            _logger = logger;
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                NationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath),
                TrailList = await _trailRepository.GetAllAsync(SD.TrailAPIPath)
        };
            return View(indexVM);
        }

        public IActionResult Privacy()
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
