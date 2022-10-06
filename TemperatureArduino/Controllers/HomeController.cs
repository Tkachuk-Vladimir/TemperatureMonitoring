using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemperatureArduino.Models;
using TemperatureArduino.Domain;
using TemperatureArduino.Domain.Entities;
using TemperatureArduino.Domain.Repositories.Abstract;
using TemperatureArduino.Domain.Repositories.EntityFramework;

namespace TemperatureArduino.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITemperatureRepository temperatureRepository;

        public HomeController(ITemperatureRepository temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;
        }

        public IActionResult Index()
        {
            var temperatureList = temperatureRepository.GetTemperature();
            return View(temperatureList);
        }

    }
}
