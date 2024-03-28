using Corona_virus_management_system.Data;
using Corona_virus_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Corona_virus_management_system.Controllers
{
    // The HomeController class controls the behavior of views related to the home page and general statistics.
    public class HomeController : Controller
    {
        private readonly Corona_virus_management_systemContext _context;

        // Constructor injection: The controller receives an instance of the context via dependency injection.
        public HomeController(Corona_virus_management_systemContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        // Displays the home page.
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Statistics
        // Displays statistics related to COVID-19 management.
        public IActionResult Statistics()
        {
            // Calculate the date range for the last month
            DateTime lastMonthStart = DateTime.Today.AddMonths(-1).Date;
            DateTime lastMonthEnd = DateTime.Today.Date;

            // Retrieve the patients who were ill in the last month
            var patients = _context.Member
                .Where(m => m.PositiveResult >= lastMonthStart && m.RecoveryDate <= lastMonthEnd)
                .ToList();

            // Create labels for all days in the month
            var labels1 = new List<string>();
            var data1 = new List<int[]>();
            for (DateTime date = lastMonthStart; date <= lastMonthEnd; date = date.AddDays(1))
            {
                labels1.Add(date.ToString("dd/MM"));
                var patientsForDay = patients.Where(p => p.PositiveResult.Value <= date.Date && p.RecoveryDate.Value >= date.Date).ToList();
                data1.Add(new[] { patientsForDay.Count });
            }

            // Calculate the number of members who weren't vaccinated at all
            var notVaccinatedMembers = _context.Member
                .Where(m => !_context.Vaccine.Any(v => v.MemberId == m.ID))
                .Count();

            // Create a chart data object
            var chartData = new
            {
                labels = labels1,
                datasets = new[]
        {
            new
            {
                label = "Sick People",
                data = data1,
                backgroundColor = "rgba(255, 99, 132, 0.2)",
                borderColor = "rgba(255, 99, 132, 1)",
                borderWidth = 1
            }
        }
            };

            ViewBag.NotVaccinatedMembers = notVaccinatedMembers;

            return View(chartData);
        }


        // GET: Home/Privacy
        // Displays the privacy policy page.
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        // Displays an error page with a unique request ID for tracking.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}