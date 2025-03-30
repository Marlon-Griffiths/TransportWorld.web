using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models;
using System.Collections.Generic;

namespace TransportWorldSystem.Web.Controllers
{
    public class DriversController : Controller
    {
        public IActionResult Index()
        {
            // Sample driver data
            var drivers = new List<Driver>
            {
                new Driver { Name = "Michael A.Johnson", Rating = 4.8, Vehicle = "Toyota Prius - Silver", ExperienceYears = 5 },
                new Driver { Name = "Sarah Thompson", Rating = 4.9, Vehicle = "Honda Accord - Blue", ExperienceYears = 7 },
                new Driver { Name = "David Wilson", Rating = 4.7, Vehicle = "Tesla Model 3 - Black", ExperienceYears = 3 }
            };

            var driversViewModel = new DriversViewModel
            {
                Title = "Meet Our Professional Drivers",
                DriversList = drivers
            };

            return View(driversViewModel);
        }
    }
}