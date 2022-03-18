using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MkvOL.Data;
using MkvOL.Models;

namespace MkvOL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BDSistemasContext _context;

        public HomeController(ILogger<HomeController> logger, BDSistemasContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var cumples = _context.Cumples.FromSqlRaw("Info_Cumpleanios").ToList();
            return View(cumples);
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
