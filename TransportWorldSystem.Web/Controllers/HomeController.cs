﻿using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models;
using System;
using System.Collections.Generic;

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

        private static readonly Dictionary<string, int> _driverArrivalTimes = new()
        {
            { "John Doe - Toyota Prius (⭐4.8)", 12 },
            { "Jane Smith - Honda Civic (⭐4.5)", 17 },
            { "Mike Brown - Nissan Altima (⭐4.7)", 9 }
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

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [HttpPost]
        public IActionResult RequestRide(RideRequestModel request)
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

                // Assign first driver
                request.DriverIndex = 0;
                string selectedDriver = _drivers[request.DriverIndex];

                request.DriverName = selectedDriver;
                request.EstimatedTime = _driverArrivalTimes[selectedDriver];
                request.DriverImageUrl = GetDriverImage(selectedDriver);

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

            if (_routes.ContainsKey(routeKey))
            {
                (double distance, double baseFare) = _routes[routeKey];
                request.Distance = distance;
                request.Fare = request.RideType == "Premium" ? baseFare + 1000 : baseFare;

                // Switch to next driver in sequence
                request.DriverIndex = (request.DriverIndex + 1) % _drivers.Count;
                string newDriver = _drivers[request.DriverIndex];

                request.DriverName = newDriver;
                request.EstimatedTime = _driverArrivalTimes[newDriver];
                request.DriverImageUrl = GetDriverImage(newDriver);

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

        private string GetDriverImage(string driverName)
        {
            if (driverName.Contains("John")) return "/images/john.jpg";
            if (driverName.Contains("Jane")) return "/images/jane.jpg";
            return "/images/mike.jpg";
        }
    }
}
