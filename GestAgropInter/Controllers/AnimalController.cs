using GestAgropInter.Models;
using GestAgropInter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestAgropInter.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animal;
        private readonly IFazendaRepository _fazenda;

        Animal ani = new Animal();
        public AnimalController(IAnimalRepository animal, IFazendaRepository fazenda)
        {
            _animal = animal;
            _fazenda = fazenda;
        }

        public IActionResult Index()
        {
            var animais = _animal.GetAllAnimal();

            return View(animais);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal animal = _animal.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }
            animal.FazendaList = _fazenda.GetAllFazenda();
            return View(animal);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ani = new Animal();
            ani.FazendaList = _fazenda.GetAllFazenda();
            return View(ani);
        }
        [HttpGet]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Animal animal)
        {

            if (ModelState.IsValid)
            {
                if (animal.Id != null)
                {
                    animal.DataAlteracao = DateTime.Now;
                    _animal.UpdateAnimal(animal);
                }                   
                else
                    _animal.AddAnimal(animal);              
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Animal animal = _animal.GetAnimal(id);
           

            if (animal == null)
            {
                return NotFound();
            }
            animal.FazendaList = _fazenda.GetAllFazenda();
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _animal.UpdateAnimal(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Animal animal = _animal.GetAnimal(id);

            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _animal.DeleteAnimal(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult VerificaTag(string tag)
        {

            if (tag == null)
            {
                return NotFound();
            }

            Animal animal = _animal.GetAnimal(tag);
            string mensagem = "yes";
            if (animal == null)
                mensagem = "not";
            return Ok(new
            {                
                mensagem = mensagem
            });
        }
    }
}