using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportWorldSystem.Web.Models;
using System.Collections.Generic;

namespace TransportWorldSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestRide(RideRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid input. Please fill out all fields.";
                return RedirectToAction("Index");
            }

            // Define fares & distances
            var faresAndDistances = new Dictionary<string, (double Distance, double Fare)>
            {
                { "Cross Roads-Halfway Tree", (2.3, 600) },
                { "Halfway Tree-Cross Roads", (2.3, 600) },
                { "Cross Roads-Downtown", (3.6, 600) },
                { "Downtown-Cross Roads", (3.6, 600) },
                { "Halfway Tree-Downtown", (5.3, 1000) },
                { "Downtown-Halfway Tree", (5.3, 1000) }
            };

            string routeKey = $"{model.PickupLocation}-{model.DropoffLocation}";

            if (faresAndDistances.ContainsKey(routeKey))
            {
                model.Distance = faresAndDistances[routeKey].Distance;
                model.Fare = model.RideType == "Premium" ? faresAndDistances[routeKey].Fare + 1000 : faresAndDistances[routeKey].Fare;
            }
            else
            {
                TempData["Error"] = "Invalid ride selection.";
                return RedirectToAction("Index");
            }

            _logger.LogInformation($"Ride requested: {model.PickupLocation} → {model.DropoffLocation}, Type: {model.RideType}, Fare: ${model.Fare}, Distance: {model.Distance} km.");

            TempData["Message"] = $"Ride requested successfully from {model.PickupLocation} to {model.DropoffLocation}.";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
