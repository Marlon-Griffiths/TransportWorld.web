using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TransportWorldSystem.Web.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace TransportWorldSystem.Web.Controllers
{
    [Authorize]
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

        private static readonly Dictionary<string, string> _maps = new()
        {
            { "Cross Roads-Downtown", "https://www.google.com/maps/d/u/0/embed?mid=1ithvLsSqcME1pVFTSnOBB9NKoBXUP5s&noprof=1" },
            { "Downtown-Cross Roads", "https://www.google.com/maps/d/u/0/embed?mid=1ekF_QApD7rnUKPT-NBuhjz5iOJGyUfk&noprof=1" },
            { "Cross Roads-Halfway Tree", "https://www.google.com/maps/d/u/0/embed?mid=1U-2w33rEnFIWxTDKYnNFp8DINCHxPAA&noprof=1" },
            { "Halfway Tree-Cross Roads", "https://www.google.com/maps/d/u/0/embed?mid=1mStHdz32nea3Xxh0HSd1aGXJHoopRZU&noprof=1" },
            { "Downtown-Halfway Tree", "https://www.google.com/maps/d/u/0/embed?mid=1ujhY3CsfTc_6tEYDx0bWSWLKZVUIA0w&noprof=1" },
            { "Halfway Tree-Downtown", "https://www.google.com/maps/d/u/0/embed?mid=1ufnHhXP8vWyBxAoTJRvXU1XO9IZhfx0&noprof=1" }
        };

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [HttpPost]
        public async Task<IActionResult> RequestRide(RideRequestModel request)
        {
            if (string.IsNullOrEmpty(request.PickupLocation) || string.IsNullOrEmpty(request.DropoffLocation))
            {
                ViewBag.ErrorMessage = "Please select valid pickup and dropoff locations.";
                return View("Index");
            }

            if (request.PickupLocation == request.DropoffLocation)
            {
                ViewBag.ErrorMessage = "Pickup and dropoff locations must be different.";
                return View("Index");
            }

            string routeKey = $"{request.PickupLocation}-{request.DropoffLocation}";

            if (_routes.ContainsKey(routeKey))
            {
                (double distance, double baseFare) = _routes[routeKey];
                request.Distance = distance;
                request.Fare = request.RideType == "Premium" ? baseFare + 1000 : baseFare;

                var drivers = await _context.Drivers.ToListAsync();
                if (!drivers.Any())
                {
                    ViewBag.ErrorMessage = "No drivers available.";
                    return View("Index");
                }

                // Random driver selection
                var random = new Random();
                request.DriverIndex = random.Next(drivers.Count);
                var selectedDriver = drivers[request.DriverIndex];

                request.DriverName = selectedDriver.Name;
                request.DriverImageUrl = selectedDriver.ImageUrl;

                ViewBag.MapUrl = _maps[routeKey];
                return View("RideDetails", request);
            }

            ViewBag.ErrorMessage = "No route available for this selection.";
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDriver(RideRequestModel request)
        {
            string routeKey = $"{request.PickupLocation}-{request.DropoffLocation}";

            if (_routes.ContainsKey(routeKey))
            {
                (double distance, double baseFare) = _routes[routeKey];
                request.Distance = distance;
                request.Fare = request.RideType == "Premium" ? baseFare + 1000 : baseFare;

                var drivers = await _context.Drivers.ToListAsync();
                if (!drivers.Any())
                {
                    ViewBag.ErrorMessage = "No drivers available.";
                    return View("Index");
                }

                // Select a random driver different from current one
                var random = new Random();
                int newIndex = request.DriverIndex;

                if (drivers.Count > 1)
                {
                    do
                    {
                        newIndex = random.Next(drivers.Count);
                    }
                    while (newIndex == request.DriverIndex);
                }

                request.DriverIndex = newIndex;
                var newDriver = drivers[request.DriverIndex];

                request.DriverName = newDriver.Name;
                request.DriverImageUrl = newDriver.ImageUrl;

                ViewBag.MapUrl = _maps[routeKey];
                return View("RideDetails", request);
            }

            ViewBag.ErrorMessage = "Unable to change driver.";
            return View("Index");
        }

        [HttpPost]
        public IActionResult RequestSent(RideRequestModel request) => View("RequestSent", request);

        [HttpGet]
        public IActionResult RequestAccepted() => View();
    }
}
