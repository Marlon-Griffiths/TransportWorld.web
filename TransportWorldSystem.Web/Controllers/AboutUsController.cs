using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models; 

namespace TransportWorldSystem.Web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            // Sample model data for the About Us page
            var aboutUsModel = new AboutUsViewModel
            {
                CompanyHistory = "Founded in 2010, Uber transformed the way people move, offering convenient and reliable transportation services globally.",
                MissionStatement = "To make transportation as reliable as running water, everywhere, for everyone.",
                CoreValues = new List<string>
                {
                    "Innovation",
                    "Customer Centricity",
                    "Safety & Trust",
                    "Environmental Sustainability",
                },
                LeadershipTeam = new List<Leader>
                {
                    new Leader { Name = "John Doe", Role = "CEO", Bio = "John has led Uber's growth from a small startup to a global leader in transportation." },
                    new Leader { Name = "Jane Smith", Role = "COO", Bio = "Jane is responsible for overseeing Uber's global operations and strategy." }
                }
            };

            return View(aboutUsModel);
        }
    }
}
