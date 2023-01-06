using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GestAgropInter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _context;
        Animal ani = new Animal();

        public HomeController(IHomeRepository context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var home = _context.GetDados();
            return View(home);
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