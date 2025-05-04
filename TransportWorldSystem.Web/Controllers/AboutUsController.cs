using Microsoft.AspNetCore.Mvc;
using TransportWorldSystem.Web.Models; 

namespace TransportWorldSystem.Web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            // the About Us page
            var aboutUsModel = new AboutUsViewModel
            {
                CompanyHistory = "Founded in 2025, Transport World has transformed the way people move, offering convenient and reliable transportation services globally.",
                MissionStatement = "To make transportation as reliable as running water, taking you to a promising future.",
                CoreValues = new List<string>
                {
                    "Innovation",
                    "Customer Centricity",
                    "Safety & Trust",
                    "Environmental Sustainability",
                },
                LeadershipTeam = new List<Leader>
                {
                    new Leader { Name = "Nolan Brown", Role = "CEO", Bio = "Nolan has led our company growth from a small startup to a global leader in transportation." },
                    new Leader { Name = "Jane Nite", Role = "COO", Bio = "Jane is responsible for overseeing Uber's global operations and strategy." }
                }
            };

            return View(aboutUsModel);
        }
    }
}
