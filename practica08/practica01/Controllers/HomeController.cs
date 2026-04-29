using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using practica01.Models;
using practica01.Repositories;
using practica01.ViewModels;
using System.Diagnostics;

namespace practica01.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DashboardRepository _dashboardRepository;

        public HomeController(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = User.Identity?.Name;

            DashboardViewModel metrics = _dashboardRepository.GetDashboardMetrics();
            return View(metrics);
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
