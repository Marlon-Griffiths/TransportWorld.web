using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportWorldSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Dictionary<string, (double Distance, double Fare)> _routes = new()
        {
            { "Cross Roads-Halfway Tree", (2.3, 600) },
            { "Halfway Tree-Cross Roads", (2.3, 600) },
            { "Cross Roads-Downtown", (3.6, 600) },
            { "Downtown-Cross Roads", (3.6, 600) },
            { "Halfway Tree-Downtown", (5.3, 1000) },
            { "Downtown-Halfway Tree", (5.3, 1000) }
        };

        private static readonly List<string> _drivers = new()
        {
            "John Doe - Toyota Prius (⭐4.8)",
            "Jane Smith - Honda Civic (⭐4.5)",
            "Mike Brown - Nissan Altima (⭐4.7)"
        };

        private static readonly Dictionary<string, string> _maps = new()
        {
            { "Cross Roads-Downtown", "https://www.google.com/maps/d/u/0/embed?mid=1ithvLsSqcME1pVFTSnOBB9NKoBXUP5s" },
            { "Downtown-Cross Roads", "https://www.google.com/maps/d/u/0/embed?mid=1ekF_QApD7rnUKPT-NBuhjz5iOJGyUfk" },
            { "Cross Roads-Halfway Tree", "https://www.google.com/maps/d/u/0/embed?mid=1U-2w33rEnFIWxTDKYnNFp8DINCHxPAA" },
            { "Halfway Tree-Cross Roads", "https://www.google.com/maps/d/u/0/embed?mid=1mStHdz32nea3Xxh0HSd1aGXJHoopRZU" },
            { "Downtown-Halfway Tree", "https://www.google.com/maps/d/u/0/embed?mid=1ujhY3CsfTc_6tEYDx0bWSWLKZVUIA0w" },
            { "Halfway Tree-Downtown", "https://www.google.com/maps/d/u/0/embed?mid=1ufnHhXP8vWyBxAoTJRvXU1XO9IZhfx0" }
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestRide(RideRequestModel request)
        {
            if (string.IsNullOrEmpty(request.PickupLocation) || string.IsNullOrEmpty(request.DropoffLocation))
            {
                ViewBag.ErrorMessage = "Please select valid locations.";
                return View("Index");
            }

            if (request.PickupLocation == request.DropoffLocation)
            {
                ViewBag.ErrorMessage = "Choose a different dropoff or pickup location.";
                return View("Index");
            }

            string routeKey = $"{request.PickupLocation}-{request.DropoffLocation}";

            if (_routes.TryGetValue(routeKey, out var info))
            {
                request.Distance = info.Distance;
                request.Fare = request.RideType == "Premium" ? info.Fare + 1000 : info.Fare;
                request.EstimatedTime = Math.Round(info.Distance / 30.0 * 60); // 30 km/h average speed

                string driver = _drivers[new Random().Next(_drivers.Count)];
                request.DriverName = driver;
                request.DriverImageUrl = GetDriverImage(driver);

                ViewBag.MapUrl = _maps[routeKey];
                return View("RideDetails", request);
            }

            ViewBag.ErrorMessage = "No route available for this selection.";
            return View("Index");
        }

        [HttpPost]
        public IActionResult ChangeDriver(RideRequestModel request)
        {
            string routeKey = $"{request.PickupLocation}-{request.DropoffLocation}";
            if (_routes.TryGetValue(routeKey, out var info))
            {
                request.Distance = info.Distance;
                request.Fare = request.RideType == "Premium" ? info.Fare + 1000 : info.Fare;
                request.EstimatedTime = Math.Round(info.Distance / 30.0 * 60);

                var availableDrivers = _drivers.Where(d => d != request.DriverName).ToList();
                string newDriver = availableDrivers[new Random().Next(availableDrivers.Count)];

                request.DriverName = newDriver;
                request.DriverImageUrl = GetDriverImage(newDriver);
                ViewBag.MapUrl = _maps[routeKey];

                return View("RideDetails", request);
            }

            ViewBag.ErrorMessage = "Unable to change driver.";
            return View("Index");
        }

        private string GetDriverImage(string driverName)
        {
            if (driverName.Contains("John")) return "/images/john.jpg";
            if (driverName.Contains("Jane")) return "/images/jane.jpg";
            return "/images/mike.jpg";
        }
    }
}
