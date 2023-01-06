using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestAgropInter.Controllers
{
    public class ColetivoController : Controller
    {
        private readonly IAnimalRepository _animal;
        private readonly IFazendaRepository _fazenda;
        Animal ani = new Animal();

        public ColetivoController(IAnimalRepository animal, IFazendaRepository fazenda)
        {
            _animal = animal;
            _fazenda = fazenda;
        }

        public IActionResult Index()
        {
            ViewBag.Fazenda = _fazenda.GetAllFazenda();
            return View();
        }
                

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}