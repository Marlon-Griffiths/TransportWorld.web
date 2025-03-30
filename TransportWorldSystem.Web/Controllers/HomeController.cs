using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransportWorldSystem.Web.Models;

namespace TransportWorldSystem.Web.Controllers;

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
    public IActionResult RequestRide(string pickupLocation, string dropoffLocation, string pickupTime, string passenger)
    {
        // Process the ride request (logging or database storage can be added here)
        _logger.LogInformation($"Ride requested from {pickupLocation} to {dropoffLocation}. Pickup: {pickupTime}, Passenger: {passenger}.");

        TempData["Message"] = $"Ride requested successfully from {pickupLocation} to {dropoffLocation}.";
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
