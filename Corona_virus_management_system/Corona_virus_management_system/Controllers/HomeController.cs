using Corona_virus_management_system.Data;
using Corona_virus_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Corona_virus_management_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly Corona_virus_management_systemContext _context;

        public HomeController(Corona_virus_management_systemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Statistics()
        {
            // Calculate the date range for the last month
            DateTime lastMonthStart = DateTime.Today.AddMonths(-1).Date;
            DateTime lastMonthEnd = DateTime.Today.Date;

            // Retrieve members who were ill in the last month
            var membersLastMonth = _context.Member
                .Where(m => m.PositiveResult >= lastMonthStart && m.RecoveryDate <= lastMonthEnd)
                .ToList();

            // Calculate the number of members who weren't vaccinated at all
            var notVaccinatedMembers = _context.Member
                .Where(m => !_context.Vaccine.Any(v => v.MemberId == m.ID))
                .Count();

            // Create a chart data object
            var chartData = new
            {
                labels = new[] { "Positive Result", "Recovery Date" },
                datasets = new[]
                {
            new
            {
                label = "Last Month",
                data = new[] { membersLastMonth.Count, membersLastMonth.Count(m => m.RecoveryDate >= lastMonthStart) },
                backgroundColor = new[] { "rgba(255, 99, 132, 0.2)", "rgba(54, 162, 235, 0.2)" },
                borderColor = new[] { "rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)" },
                borderWidth = 1
            }
                }
            };

            ViewBag.NotVaccinatedMembers = notVaccinatedMembers;

            return View(chartData);
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