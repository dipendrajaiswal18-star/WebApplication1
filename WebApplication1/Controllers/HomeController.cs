using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGuidService _guidService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IGuidService guidService, ILogger<HomeController> logger)
        {
            _guidService = guidService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var guid = _guidService.GetGuid();
            _logger.LogInformation("Home - Guid from GuidService: {Guid}", guid);
            ViewBag.Guid = guid;
            return View();
        }

        public IActionResult Privacy()
        {
            var guid = _guidService.GetGuid();
            _logger.LogInformation("Privacy - Guid from GuidService: {Guid}", guid);
            ViewBag.Guid = guid;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
