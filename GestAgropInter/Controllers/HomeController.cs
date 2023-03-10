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
        private IHttpContextAccessor _contexto;
        Animal ani = new Animal();

        public HomeController(IHomeRepository context, IHttpContextAccessor contexto)
        {
            _context = context;
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            var ip = this._contexto.HttpContext.Connection.RemoteIpAddress.ToString();
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